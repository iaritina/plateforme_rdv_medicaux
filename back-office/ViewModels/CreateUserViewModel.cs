using System.ComponentModel.DataAnnotations;

namespace back_office.ViewModels;

public class CreateUserViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Nom d'utilisateur")]
    public string UserName { get; set; }

    [Display(Name = "Téléphone")]
    [Phone]
    public string PhoneNumber { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Mot de passe")]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirmer le mot de passe")]
    [Compare("Password", ErrorMessage = "Le mot de passe et la confirmation ne correspondent pas.")]
    public string ConfirmPassword { get; set; }

    public string? Role { get; set; } // "Patient", "Doctor", "Admin"
}