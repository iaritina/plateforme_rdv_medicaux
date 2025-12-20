using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace back_office.Models;

public class DoctorSpeciality
{
    public int DoctorId { get; set; }
    public int SpecialityId { get; set; }

    // Navigation properties
    
    [ValidateNever]
    [ForeignKey(nameof(DoctorId))]
    public Doctor Doctor { get; set; } = null!;
    [ValidateNever]
    [ForeignKey(nameof(SpecialityId))]
    public Specialities Speciality { get; set; } = null!;
    
}