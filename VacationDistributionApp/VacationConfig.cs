namespace VacationDistributionApp;

public class VacationConfig
{
    public DateTime StartYearDay => new(DateTime.Now.Year, 1, 1);
    public DateTime EndYearDate => new(DateTime.Now.Year, 12, 31);
    public int DaysCountOfYear => DateTime.IsLeapYear(StartYearDay.Year) ? 366 : 365;
    public int CountOfVacation { get; set; }
    public int VacationStep { get; set; }
    public bool CanBeCollision { get; set; }
    public int BreakBetweenHolidaysInDays { get; set; }
    public List<DayOfWeek> WorkingDays { get; set; }

    public VacationConfig(int countOfVacation, bool canBeCollision, int breakBetweenHolidaysInDays, List<DayOfWeek> workingDays)
    {
        CountOfVacation = countOfVacation;
        CanBeCollision = canBeCollision;
        BreakBetweenHolidaysInDays = breakBetweenHolidaysInDays;
        WorkingDays = workingDays;
    }
}