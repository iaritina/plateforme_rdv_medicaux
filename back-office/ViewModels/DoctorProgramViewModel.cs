using back_office.Models;

namespace back_office.ViewModels;

public class DoctorProgramViewModel
{
    public int DoctorId {get; set;}
    public string DoctorName { get; set; } = null!;
    
    public WeekDay Day { get; set; }
    public string DayName { get; set; } = null!;
    public List<DoctorDayProgramViewModel> Days  { get; set; } = new();
}