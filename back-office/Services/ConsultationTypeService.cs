using back_office.Data;
using back_office.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace back_office.Services;

public class ConsultationTypeService
{
    private readonly ApplicationDbContext _context;
    
    public  ConsultationTypeService(ApplicationDbContext context)
    {
        _context = context;
    }
   

    public async Task<List<ConsultationType>> GetAllConsulType(int pageNumber, int pageSize)
    {
        return await _context.ConsultationTypes
            .Include(c=>c.Speciality)
            .OrderBy(ct => ct.IdTypeConsul)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();;
    }
    
    public async Task<List<ConsultationType>> GetAvailableForAppointmentAsync()
    {
        return await _context.ConsultationTypes
            .Include(ct => ct.Speciality)
            .OrderBy(ct => ct.NameTypeConsul)
            .ToListAsync();
    }
    
    public async Task<int> GetTotalConsulTypeCount()
    {
        return await _context.ConsultationTypes.CountAsync();
    }

    public void AddConsultationType(ConsultationType consultationType)
    {
        try
        {
            _context.ConsultationTypes.Add(consultationType);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception("Creating Consultation Type Failed", e);
        }
    }
    
    public async Task<ConsultationType?> GetByIdAsync(int id)
    {
        return await _context.ConsultationTypes
            .Include(c => c.Speciality)
            .FirstOrDefaultAsync(c => c.IdTypeConsul == id);
    }
    
    public async Task UpdateAsync(ConsultationType consultationType)
    {
        var existing = await _context.ConsultationTypes
            .FirstOrDefaultAsync(c => c.IdTypeConsul == consultationType.IdTypeConsul);

        if (existing == null)
            throw new Exception("Type de consultation introuvable");

        existing.NameTypeConsul = consultationType.NameTypeConsul;
        existing.AvgDuration = consultationType.AvgDuration;
        existing.IdSpec = consultationType.IdSpec;

        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var entity = await _context.ConsultationTypes.FindAsync(id);
        if (entity != null)
        {
            _context.ConsultationTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
    
}