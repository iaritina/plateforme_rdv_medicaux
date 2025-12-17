using back_office.Services;
using back_office.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace back_office.Controllers;

public class ConsultationTypeController : Controller
{
    private readonly ILogger<ConsultationTypeController> _logger;

    private readonly ConsultationTypeService _service;
    private readonly SpecialityService _specialityService;

    public ConsultationTypeController(ConsultationTypeService service,
        ILogger<ConsultationTypeController> logger, SpecialityService specialityService)
    {
        _service = service;
        _logger = logger;
        _specialityService = specialityService;
    }

    // GET
    public async Task<IActionResult> Index(int page = 1)
    {
        int pageSize = 10;
        var typeConsul = await _service.GetAllConsulType(page, pageSize);
        var totalConsulType = await _service.GetTotalConsulTypeCount();
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(totalConsulType / (double)pageSize);

        return View(typeConsul);
    }

    public async Task<IActionResult> Create()
    {
        var vm = new ConsultationTypeCreateViewModel
        {
            Specialities = await _specialityService.GetSpecialitiesForSelectAsync()
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ConsultationTypeCreateViewModel vm)
    {
        if (ModelState.IsValid)
        {
            _service.AddConsultationType(vm.ConsultationType);
            _logger.LogInformation("Consultation Type created");
            return RedirectToAction(nameof(Index));
        }

        return View(vm);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity == null)
            return NotFound();

        var vm = new ConsultationTypeCreateViewModel
        {
            ConsultationType = entity,
            Specialities = await _specialityService.GetSpecialitiesForSelectAsync()
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ConsultationTypeCreateViewModel vm)
    {
        Console.WriteLine("ID ConsulType: " + id + " ConsultationType: " + vm.ConsultationType.IdTypeConsul);
        if (id != vm.ConsultationType.IdTypeConsul)
            return BadRequest();

        if (!ModelState.IsValid)
        {
            vm.Specialities = await _specialityService.GetSpecialitiesForSelectAsync();
            return View(vm);
        }

        await _service.UpdateAsync(vm.ConsultationType);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity == null)
            return NotFound();

        return View(entity);
    }
    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}