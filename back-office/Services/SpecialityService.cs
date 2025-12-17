using back_office.Data;
using back_office.Models;

namespace back_office.Services;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SpecialityService
{
    private readonly ApplicationDbContext _context;

    public SpecialityService(ApplicationDbContext context)
    {
        _context = context;
    }

    // READ - List
    public async Task<List<Specialities>> GetAllAsync()
    {
        return await _context.Specialities.ToListAsync();
    }

    // READ - By Id
    public async Task<Specialities?> GetByIdAsync(int id)
    {
        return await _context.Specialities
            .FirstOrDefaultAsync(s => s.IdSpec == id);
    }

    // CREATE
    public async Task CreateAsync(Specialities speciality)
    {
        _context.Specialities.Add(speciality);
        await _context.SaveChangesAsync();
    }

    // UPDATE
    public async Task UpdateAsync(Specialities speciality)
    {
        _context.Specialities.Update(speciality);
        await _context.SaveChangesAsync();
    }

    // DELETE
    public async Task DeleteAsync(int id)
    {
        var speciality = await GetByIdAsync(id);
        if (speciality != null)
        {
            _context.Specialities.Remove(speciality);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Specialities.AnyAsync(s => s.IdSpec == id);
    }
}
