using back_office.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace back_office.ViewModels;

public class ConsultationTypeCreateViewModel
{
    public ConsultationType ConsultationType { get; set; } = new();

    // Liste des spécialités (pour le select)
    public List<SelectListItem> Specialities { get; set; } = new();
}