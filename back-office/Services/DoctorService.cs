using back_office.Data;
using back_office.Models;
using Microsoft.EntityFrameworkCore;

namespace back_office.Services;

public class DoctorService
{
    private readonly ApplicationDbContext _context;

    public DoctorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Doctor>> GetDoctorsPaged(int pageNumber, int pageSize)
    {
        return await _context.Doctors
            .OrderBy(d => d.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalDoctorsCount()
    {
        return await _context.Doctors.CountAsync();
    }


    public Doctor? GetDoctor(int id)
    {
        if (id < 1)
            return null;
        try
        {
            return _context.Doctors.Find(id) ?? throw new Exception("Doctor with id :" + id + " not found");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }

    public void AddDoctor(Doctor doctor)
    {
        try
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }

    public Doctor UpdateDoctor(Doctor updatedDoctor)
    {
        try
        {
            _context.Doctors.Update(updatedDoctor);
            _context.SaveChanges();

            return updatedDoctor;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }

    public void DeleteDoctor(int id)
    {
        Doctor? doctor = GetDoctor(id);
        if (doctor == null) throw new Exception("Doctor not found");
        _context.Doctors.Remove(doctor);
        _context.SaveChanges();
    }
}