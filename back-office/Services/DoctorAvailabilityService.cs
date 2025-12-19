using back_office.Data;
using back_office.Models;
using back_office.ViewModels;

namespace back_office.Services;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class DoctorAvailabilityService
{
    private readonly ApplicationDbContext _context;

    public DoctorAvailabilityService(ApplicationDbContext context)
    {
        _context = context;
    }

    // READ - All
    public async Task<List<DoctorAvailability>> GetAllAsync()
    {
        return await _context.DoctorAvailabilities.ToListAsync();
    }

    // READ - By ID
    public async Task<DoctorAvailability?> GetByIdAsync(int id)
    {
        return await _context.DoctorAvailabilities
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    // READ - By Doctor
    public async Task<List<DoctorAvailability>> GetByDoctorAsync(int doctorId)
    {
        return await _context.DoctorAvailabilities
            .Where(a => a.DoctorId == doctorId)
            .OrderBy(a => a.DayWeek)
            .ThenBy(a => a.StartTime)
            .ToListAsync();
    }

    // CREATE
    public async Task CreateAsync(DoctorAvailability availability)
    {
        _context.DoctorAvailabilities.Add(availability);
        await _context.SaveChangesAsync();
    }

    // UPDATE
    public async Task UpdateAsync(DoctorAvailability availability)
    {
        _context.DoctorAvailabilities.Update(availability);
        await _context.SaveChangesAsync();
    }

    // DELETE
    public async Task DeleteAsync(int id)
    {
        var availability = await GetByIdAsync(id);
        if (availability == null)
            return;

        _context.DoctorAvailabilities.Remove(availability);
        await _context.SaveChangesAsync();
    }

    public async Task<DoctorProgramViewModel?> GetDoctorPrograms(int doctorId)
    {
        var availabilities = await _context.DoctorAvailabilities
            .Include(a => a.Doctor)
            .Where(a => a.DoctorId == doctorId)
            .OrderBy(a => a.DayWeek)
            .ThenBy(a => a.StartTime)
            .ToListAsync();

        if (!availabilities.Any())
            return null;

        var groupedByDay = availabilities
            .GroupBy(a => a.DayWeek)
            .ToDictionary(g => g.Key, g => g.ToList());

        var program = new DoctorProgramViewModel
        {
            DoctorId = doctorId,
            DoctorName = availabilities.First().Doctor.FullName
        };

        for (WeekDay day = WeekDay.Lundi; day <= WeekDay.Vendredi; day++)
        {
            groupedByDay.TryGetValue(day, out var dayAvailabilities);

            var slots = dayAvailabilities?
                            .Select(a => new TimeSlotViewModel
                            {
                                StartTime = a.StartTime,
                                EndTime = a.EndTime
                            })
                            .ToList()
                        ?? new List<TimeSlotViewModel>();

            program.Days.Add(new DoctorDayProgramViewModel
            {
                Day = day,
                DayName = day.ToString(),
                TimeSlots = slots
            });
        }

        return program;
    }

}