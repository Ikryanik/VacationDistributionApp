namespace VacationDistributionApp;

public class EmployeeVacation
{
    public Employee Employee { get; set; } = null!;
    public List<DateTime> Vacations { get; set; } = new();
}
    