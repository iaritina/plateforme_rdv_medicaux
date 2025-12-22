using back_office.Data;
using back_office.Models;
using Microsoft.EntityFrameworkCore;

namespace back_office.Services;

public class AppointmentService
{
    private readonly ApplicationDbContext _context;

    public AppointmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    // CREATE
    public async Task<Appointment> CreateAsync(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    // READ ALL
    public async Task<List<Appointment>> GetAllAsync()
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.ConsultationType)
            .Include(a => a.Doctor)
            .ToListAsync();
    }

    // READ BY ID
    public async Task<Appointment?> GetByIdAsync(int id)
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.ConsultationType)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(a => a.ID_APT == id);
    }

    // UPDATE
    public async Task<bool> UpdateAsync(int id, Appointment updated)
    {
        var existing = await _context.Appointments.FindAsync(id);
        if (existing == null) return false;

        existing.ID_PAT = updated.ID_PAT;
        existing.ID_TYPE_CONSUL = updated.ID_TYPE_CONSUL;
        existing.ID_DOC = updated.ID_DOC;
        existing.DATE_START_TIME = updated.DATE_START_TIME;
        existing.STATUS = updated.STATUS;
        existing.PRIORITY = updated.PRIORITY;

        await _context.SaveChangesAsync();
        return true;
    }

    // DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null) return false;

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
        return true;
    }
    
    private static WeekDay? ToWeekDay(DateTime date)
    {
        return date.DayOfWeek switch
        {
            DayOfWeek.Monday => WeekDay.Lundi,
            DayOfWeek.Tuesday => WeekDay.Mardi,
            DayOfWeek.Wednesday => WeekDay.Mercredi,
            DayOfWeek.Thursday => WeekDay.Jeudi,
            DayOfWeek.Friday => WeekDay.Vendredi,
            _ => null // weekend ignoré
        };
    }


    public async Task<Appointment?> ScheduleAppointmentAsync(
        int consultationTypeId,
        int? patientId,
        DateTime startSearchDate)
    {
        // 1. Charger le type de consultation + spécialité
        var consultationType = await _context.ConsultationTypes
            .Include(ct => ct.Speciality)
            .FirstOrDefaultAsync(ct => ct.IdTypeConsul == consultationTypeId);

        if (consultationType == null)
            return null;

        var specialityId = consultationType.Speciality.IdSpec;

        // 2. Docteurs ayant cette spécialité
        var doctors = await _context.DoctorSpecialities
            .Where(ds => ds.SpecialityId == specialityId)
            .Select(ds => ds.Doctor)
            .Distinct()
            .ToListAsync();

        if (!doctors.Any())
            return null;

        // 3. Disponibilités
        var availabilities = await _context.DoctorAvailabilities
            .Where(a => doctors.Select(d => d.Id).Contains(a.DoctorId))
            .ToListAsync();

        // 4. Recherche sur les 14 prochains jours (modifiable)
        for (int i = 0; i < 14; i++)
        {
            var date = startSearchDate.Date.AddDays(i);
            var weekDay = (WeekDay)date.DayOfWeek;

            foreach (var doctor in doctors)
            {
                var doctorAvailabilities = availabilities
                    .Where(a => a.DoctorId == doctor.Id && a.DayWeek == weekDay);

                foreach (var av in doctorAvailabilities)
                {
                    var slotStart = date.Add(av.StartTime);
                    var slotEnd = date.Add(av.EndTime);

                    // créneau libre ?
                    var duration = (double) consultationType.AvgDuration!;

                    var hasConflict = await _context.Appointments.AnyAsync(a =>
                        a.ID_DOC == doctor.Id &&
                        a.DATE_START_TIME < slotEnd &&
                        a.DATE_START_TIME.AddMinutes(duration) > slotStart
                    );

                    if (!hasConflict)
                    {
                        var appointment = new Appointment
                        {
                            ID_PAT = patientId,
                            ID_TYPE_CONSUL = consultationTypeId,
                            ID_DOC = doctor.Id,
                            DATE_START_TIME = slotStart,
                            STATUS = "PLANNED",
                            PRIORITY = 1
                        };

                        _context.Appointments.Add(appointment);
                        await _context.SaveChangesAsync();

                        return appointment;
                    }
                }
            }
        }

        return null;
    }

}