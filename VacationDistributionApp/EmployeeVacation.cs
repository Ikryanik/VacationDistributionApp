namespace VacationDistributionApp;

public class EmployeeVacation
{
    public Employee Employee { get; set; }
    public List<DateTime> vacations { get; set; } = new List<DateTime>();
}
    