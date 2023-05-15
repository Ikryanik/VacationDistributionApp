namespace VacationDistributionApp;

public static class VacationService
{
    public static readonly Random Random = new();

    public static DateTimeRange? GetVacation(List<DateTime> dates, VacationConfig config, HolidaysByWorkingCalendar calendar)
    {
        var range = new DateTimeRange();

        var randomDays = Random.Next(0, config.DaysCountOfYear);

        var randomStartDay = config.StartYearDay.AddDays(randomDays);

        var isWorkingDay = ValidateWorkingDay(randomStartDay, calendar, config);
        if (!isWorkingDay) return null;

        range.StartDateTime = randomStartDay;
        range.EndDateTime = range.StartDateTime.AddDays(config.VacationStep);

        var holidaysInVacation = ValidateCollisionWithHolidays(range, calendar);
        if (holidaysInVacation > 0)
        {
            range.EndDateTime = range.EndDateTime.AddDays(holidaysInVacation);
        }
        
        var isOnlyVacationForPeriod = ValidateCollisionWithVacationForPeriod(range, config.BreakBetweenHolidaysInDays, dates);

        if (!isOnlyVacationForPeriod) return null;

        return range;
    }

    private static bool ValidateWorkingDay(DateTime startDate, HolidaysByWorkingCalendar calendar, VacationConfig config)
    {
        var isWeekend = config.WorkingDays.Contains(startDate.DayOfWeek);
        var isHoliday = calendar.HolidaysDates.Contains(startDate);

        return !isWeekend && !isHoliday;
    }

    private static bool ValidateCollisionWithOtherDates(DateTimeRange range, List<DateTime> employeeVacationDates)
    {
        var isCollisionWithDates =
            employeeVacationDates.Any(element => element >= range.StartDateTime && element <= range.EndDateTime);

        return isCollisionWithDates;
    }

    private static bool ValidateCollisionWithVacationForPeriod(DateTimeRange range, int breakBetweenVacation, List<DateTime> employeeVacationDates)
    {
        var existStart = employeeVacationDates.Any(x =>
            x.AddDays(breakBetweenVacation) >= range.StartDateTime &&
            x.AddDays(breakBetweenVacation) >= range.EndDateTime);

        var existEnd = employeeVacationDates.Any(x =>
            x.AddDays(-breakBetweenVacation) <= range.StartDateTime &&
            x.AddDays(-breakBetweenVacation) <= range.EndDateTime);

        return !existStart || !existEnd;
    }

    public static bool ValidateCollisionWithOtherEmployees(DateTimeRange range, List<EmployeeVacation> allVacation, VacationConfig config)
    {
        var isCollision = false;
        var isBreakBetweenVacationOfDifferentEmployees = false;
        foreach (var vacation in allVacation)
        {
            isCollision = ValidateCollisionWithOtherDates(range, vacation.vacations);

            if (isCollision) break;

            isBreakBetweenVacationOfDifferentEmployees = vacation.vacations.Any(element =>
                element.AddDays(3) >= range.StartDateTime && element.AddDays(3) <= range.EndDateTime);
        }
        
        return isCollision && isBreakBetweenVacationOfDifferentEmployees;
    }

    private static int ValidateCollisionWithHolidays(DateTimeRange range, HolidaysByWorkingCalendar calendar)
    {
        var count = 0;
        var holidays = calendar.HolidaysDates;
        for (var vacationDate = range.StartDateTime; vacationDate < range.EndDateTime; vacationDate = vacationDate.AddDays(1))
        {
            var isHoliday = holidays.Contains(vacationDate);
            if (isHoliday)
            {
                count++;
            }
        }

        return count;
    }
}