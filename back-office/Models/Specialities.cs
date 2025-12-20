namespace back_office.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Specialities
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdSpec { get; set; }

    [StringLength(80)]
    public string? NameSpec { get; set; }
    
    public ICollection<ConsultationType> ConsultationTypes { get; set; }
        = new List<ConsultationType>();

    public ICollection<DoctorSpeciality> DoctorSpecialities { get; set; } = new List<DoctorSpeciality>();
}