using back_office.Data;
using back_office.Models;
using Microsoft.EntityFrameworkCore;

namespace back_office.Services;

public class DoctorSpecialityService
{
    private readonly ApplicationDbContext _context;

    public DoctorSpecialityService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(int doctorId, int specialityId)
    {
        var exists = await _context.DoctorSpecialities
            .AnyAsync(ds =>
                ds.DoctorId == doctorId &&
                ds.SpecialityId == specialityId);

        if (exists)
            return;

        var entity = new DoctorSpeciality
        {
            DoctorId = doctorId,
            SpecialityId = specialityId
        };

        _context.DoctorSpecialities.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int doctorId, int specialityId)
    {
        var entity = await _context.DoctorSpecialities
            .FirstOrDefaultAsync(ds =>
                ds.DoctorId == doctorId &&
                ds.SpecialityId == specialityId);

        if (entity == null)
            return;

        _context.DoctorSpecialities.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Specialities>> GetSpecialitiesByDoctorAsync(int doctorId)
    {
        return await _context.DoctorSpecialities
            .Where(ds => ds.DoctorId == doctorId)
            .Select(ds => ds.Speciality)
            .ToListAsync();
    }

    public async Task<List<Doctor>> GetDoctorsBySpecialityAsync(int specialityId)
    {
        return await _context.DoctorSpecialities
            .Where(ds => ds.SpecialityId == specialityId)
            .Select(ds => ds.Doctor)
            .ToListAsync();
    }
}