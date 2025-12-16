using System.ComponentModel.DataAnnotations;

namespace back_office.Models;

public class Patient
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Nom complet")]
    public string FullName { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Date de naissance")]
    [CustomValidation(typeof(Patient), nameof(ValidateBirthDate))]
    public DateTime DateBirth { get; set; }
    
    [Required]
    [Display(Name = "Genre")]
    public Gender Gender { get; set; }
    
    public static ValidationResult ValidateBirthDate(DateTime date, ValidationContext context)
    {
        if (date > DateTime.Today)
        {
            return new ValidationResult("La date de naissance ne peut pas être dans le futur.");
        }

        if (date < DateTime.Today.AddYears(-120))
        {
            return new ValidationResult("La date de naissance est trop ancienne.");
        }

        return ValidationResult.Success;
    }


}