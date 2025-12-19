using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace back_office.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("DOCTOR_AVAILABILITY")]
public class DoctorAvailability
{
    [Key]
    [Column("ID_AVBL")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("DAY_WEEK")]
    [Required]
    public WeekDay DayWeek { get; set; }

    [Column("START_TIME")]
    [Required]
    public TimeSpan StartTime { get; set; }

    [Column("END_TIME")]
    [Required]
    public TimeSpan EndTime { get; set; }

    [Column("ID_DOC")]
    [Required]
    public int DoctorId { get; set; }

    [ValidateNever]
    [ForeignKey(nameof(DoctorId))]
    public Doctor Doctor { get; set; } = null!;


}
