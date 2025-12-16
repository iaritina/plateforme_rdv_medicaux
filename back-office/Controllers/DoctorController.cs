using back_office.Models;
using back_office.Services;
using Microsoft.AspNetCore.Mvc;

namespace back_office.Controllers;

public class DoctorController : Controller
{
    private readonly ILogger<DoctorController> _logger;

    private readonly DoctorService _service;

    public DoctorController(DoctorService service, ILogger<DoctorController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // GET
    public async Task<IActionResult> Doctors(int page = 1)
    {
        int pageSize = 10;

        var doctors = await _service.GetDoctorsPaged(page, pageSize);
        var totalDoctors = await _service.GetTotalDoctorsCount();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(totalDoctors / (double)pageSize);

        return View(doctors);
    }

    public IActionResult DoctorCreateView()
    {
        return View();
    }

    public IActionResult StoreDoctor(Doctor doctor)
    {
        try
        {
            _service.AddDoctor(doctor);
            _logger.LogInformation(doctor.FullName, "Doctor created");
            return RedirectToAction("Doctors");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Doctor creation failed");
            throw new Exception("Doctor creation failed : " + e.Message);
        }
    }

    public IActionResult DoctorEditView(int id)
    {
        Doctor? doctor = _service.GetDoctor(id);
        return View(doctor);
    }

    public IActionResult UpdateDoctor(Doctor updatedDoctor)
    {
        try
        {
            _service.UpdateDoctor(updatedDoctor);
            _logger.LogInformation("Doctor with ID: " + updatedDoctor.Id + " has been updated");
            return RedirectToAction("DoctorEditView", new { id = updatedDoctor.Id });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Doctor update failed");
            throw new Exception("Doctor updating failed : " + e.Message);
        }
    }

    public IActionResult DeleteDoctor(int id)
    {
        _service.DeleteDoctor(id);
        _logger.LogInformation("Doctor with ID: " + id + " has been deleted");
        return RedirectToAction("Doctors");
    }
}