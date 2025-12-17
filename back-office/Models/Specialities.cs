namespace back_office.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Specialities
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int IdSpec { get; set; }

    [StringLength(80)]
    public string? NameSpec { get; set; }
}