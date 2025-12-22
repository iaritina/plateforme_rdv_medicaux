namespace back_office.ViewModels;

public class ConsultationTypeListVm
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string SpecialityName { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
}