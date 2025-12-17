using back_office.Data;
using back_office.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        return await _context.Specialities
            .Include(s => s.ConsultationTypes)
            .ToListAsync();
    }

    // READ - By Id
    public async Task<Specialities?> GetByIdAsync(int id)
    {
        return await _context.Specialities
            .Include(s =>s.ConsultationTypes)
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

        if (speciality != null && speciality.ConsultationTypes.Any())
            throw new Exception("Unable to delete speciality");
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
    
    public async Task<List<SelectListItem>> GetSpecialitiesForSelectAsync()
    {
        return await _context.Specialities
            .OrderBy(s => s.NameSpec)
            .Select(s => new SelectListItem
            {
                Value = s.IdSpec.ToString(),
                Text = s.NameSpec
            })
            .ToListAsync();
    }
}
