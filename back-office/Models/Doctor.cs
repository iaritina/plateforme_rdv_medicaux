using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_office.Models
{
    [Table("DOCTORS")]
    public class Doctor
    {
        [Key] [Column("ID_DOC")] public int Id { get; set; }

        [Required]
        [Column("DOC_NAME")]
        [StringLength(80)]
        public string? FullName { get; set; }

        [Column("EMAIL")] [StringLength(255)] public string? Email { get; set; }

        [Required]
        [Column("CONTACT")]
        [StringLength(80)]
        public string? Contact { get; set; }

        [Column("IS_DISABLED")] public int? IsDisabled { get; set; } = 0;
        
        public ICollection<DoctorAvailability> Availabilities { get; set; } = new List<DoctorAvailability>();
    }
}