using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace back_office.ViewModels;

public class AppointmentCreateVm
{
    [Required]
    [Display(Name = "Type de consultation")]
    public int ConsultationTypeId { get; set; }

    [Display(Name = "À partir du")]
    [DataType(DataType.Date)]
    public DateTime? PreferredStartDate { get; set; }

    [Range(1, 3)]
    public int Priority { get; set; } = 1;

    // Pour l'affichage du select
    public List<SelectListItem> ConsultationTypes { get; set; }
        = new();
}