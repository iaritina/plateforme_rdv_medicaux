using back_office.Models;
using back_office.Services;
using Microsoft.AspNetCore.Mvc;

namespace back_office.Controllers;

public class SpecialityController : Controller
{
     private readonly SpecialityService _service;

    public SpecialityController(SpecialityService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var specialities = await _service.GetAllAsync();
        return View(specialities);
    }

    public async Task<IActionResult> Details(int id)
    {
        var speciality = await _service.GetByIdAsync(id);
        if (speciality == null)
            return NotFound();

        return View(speciality);
    }

    public IActionResult Create()
    {
        return View();
    }

    // CREATE (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Specialities speciality)
    {
        if (ModelState.IsValid)
        {
            await _service.CreateAsync(speciality);
            return RedirectToAction(nameof(Index));
        }
        return View(speciality);
    }

    // UPDATE (GET)
    public async Task<IActionResult> Edit(int id)
    {
        var speciality = await _service.GetByIdAsync(id);
        if (speciality == null)
            return NotFound();

        return View(speciality);
    }

    // UPDATE (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Specialities speciality)
    {
        if (ModelState.IsValid)
        {
            await _service.UpdateAsync(speciality);
            return RedirectToAction(nameof(Index));
        }
        return View(speciality);
    }

    // DELETE (POST)
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}