using back_office.Models;

namespace back_office.ViewModels;

public class DoctorDayProgramViewModel
{
    public WeekDay Day { get; set; }
    public string DayName { get; set; } = string.Empty;
    public List<TimeSlotViewModel> TimeSlots { get; set; } = new();
}