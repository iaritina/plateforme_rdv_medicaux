using back_office.Models;
using back_office.Services;
using back_office.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace back_office.Controllers;

public class AppointmentsController : Controller
{
    // GET
    private readonly AppointmentService _service;
    private readonly ConsultationTypeService _consultationTypeService;

    public AppointmentsController(AppointmentService service, ConsultationTypeService consultationTypeService)
    {
        _service = service;
        _consultationTypeService = consultationTypeService;
    }

    // GET: /Appointments
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var appointments = await _service.GetAllAsync();
        return View(appointments);
    }

    // GET: /Appointments/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var appointment = await _service.GetByIdAsync(id);

        if (appointment == null)
            return NotFound();

        return Json(appointment);
    }
    private static List<SelectListItem> MapToSelectList(
        IEnumerable<ConsultationType> types)
    {
        return types.Select(ct => new SelectListItem
        {
            Value = ct.IdTypeConsul.ToString(),
            Text = $"{ct.NameTypeConsul} ({ct.Speciality.NameSpec})"
        }).ToList();
    }
    public async Task<IActionResult> Create()
    {
        var consultationTypes =
            await _consultationTypeService.GetAvailableForAppointmentAsync();

        var vm = new AppointmentCreateVm
        {
            ConsultationTypes = MapToSelectList(consultationTypes)
        };

        return View(vm);
    }
    // POST: /Appointments/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AppointmentCreateVm vm)
    {
        if (!ModelState.IsValid)
        {
            var types =
                await _consultationTypeService.GetAvailableForAppointmentAsync();

            vm.ConsultationTypes = MapToSelectList(types);
            return View(vm);
        }

        var patientId = 1;

        var appointment = await _service.ScheduleAppointmentAsync(
            vm.ConsultationTypeId,
            patientId,
            vm.PreferredStartDate ?? DateTime.Today
        );

        if (appointment == null)
        {
            ModelState.AddModelError("", "Aucun créneau disponible.");
            var types =
                await _consultationTypeService.GetAvailableForAppointmentAsync();
            vm.ConsultationTypes = MapToSelectList(types);

            return View(vm);
        }

        return RedirectToAction("Success");
    }

    // POST: /Appointments/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Appointment appointment)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _service.UpdateAsync(id, appointment);
        if (!success)
            return NotFound();

        return Ok();
    }

    // POST: /Appointments/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success)
            return NotFound();

        return Ok();
    }
}