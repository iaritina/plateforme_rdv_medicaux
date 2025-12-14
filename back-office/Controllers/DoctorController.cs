using back_office.Models;
using Microsoft.AspNetCore.Mvc;

namespace back_office.Controllers;

public class DoctorController : Controller
{
    // GET
    public IActionResult Doctors()
    {
        return View();
    }
    
    public IActionResult DoctorCreateView()
    {
        return View();
    }
    
    public IActionResult StoreDoctor(Doctor doctor)
    {
        return RedirectToAction("Doctors");
    }
}