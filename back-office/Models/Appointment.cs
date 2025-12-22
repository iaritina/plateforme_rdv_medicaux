using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_office.Models;

public class Appointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID_APT { get; set; }

    // ===== Relations =====
    public int? ID_PAT { get; set; }
    public Patient? Patient { get; set; }

    [Required]
    public int ID_TYPE_CONSUL { get; set; }
    public ConsultationType ConsultationType { get; set; } = null!;

    [Required]
    public int ID_DOC { get; set; }
    public Doctor Doctor { get; set; } = null!;

    // ===== Champs métier =====
    [Required]
    public DateTime DATE_START_TIME { get; set; }


    [Required]
    [Column(TypeName = "char(10)")]
    public string STATUS { get; set; } = string.Empty;

    [Required]
    public int PRIORITY { get; set; }
}