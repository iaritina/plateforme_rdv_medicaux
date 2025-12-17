using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace back_office.Models;

public class ConsultationType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdTypeConsul { get; set; }

    [Column("ID_SPEC")] public int IdSpec { get; set; }

    [StringLength(255)] public string? NameTypeConsul { get; set; }

    public int? AvgDuration { get; set; }

    [ValidateNever]
    [ForeignKey(nameof(IdSpec))]
    public Specialities Speciality { get; set; } = null!;
}