using System.ComponentModel.DataAnnotations;

namespace back_office.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required, Display(Name = "Nom d'utilisateur"), StringLength(100)]
    public required string Username { get; set; }

    [Required, EmailAddress, StringLength(255)]
    public required string Email { get; set; }

    [Required, DataType(DataType.Password), StringLength(100)]
    public required string Password { get; set; }
}