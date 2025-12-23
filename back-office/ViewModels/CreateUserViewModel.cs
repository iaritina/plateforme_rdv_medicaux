using System.ComponentModel.DataAnnotations;

namespace back_office.ViewModels;

public class CreateUserViewModel
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string? Role { get; set; } // "Patient", "Doctor", "Admin"
}