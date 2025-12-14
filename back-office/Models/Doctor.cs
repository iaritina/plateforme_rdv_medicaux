

using System.ComponentModel.DataAnnotations;

namespace back_office.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? Email { get; set; }
    }
}