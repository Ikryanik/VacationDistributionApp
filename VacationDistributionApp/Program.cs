using VacationDistributionApp;

var employeeVacationList = new List<EmployeeVacation>
{
    new()
    {
        Employee = new Employee("Прокофьев", "Прокофий", "Прокофьевич"), vacations = new List<DateTime>()
    },
    new()
    {
        Employee = new Employee("Розмаринова", "Розмарина", "Розмариновна"), vacations = new List<DateTime>()
    },
    new()
    {
        Employee = new Employee("Сидоров", "Сидор", "Сидорович"), vacations = new List<DateTime>()
    }
};

var vacationStepsOption = new[] { 7, 14 };

var availableWorkingDaysOfWeek = new List<DayOfWeek>
    { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
var calendar = new HolidaysByWorkingCalendar();

var config = new VacationConfig(28, false, 30, availableWorkingDaysOfWeek);

foreach (var employeeVacation in employeeVacationList)
{
    var vacationAmount = config.CountOfVacation;
    var countVacation = 0;

    while (countVacation < vacationAmount)
    {
        if (countVacation == 21)
        {
            config.VacationStep = 7;
        }
        else
        {
            var randomIndex = VacationService.Random.Next(0, vacationStepsOption.Length);
            config.VacationStep = vacationStepsOption[randomIndex];
        }

        var vacationRange = VacationService.GetVacation(employeeVacation.vacations, config, calendar);
        if (vacationRange == null) continue;

        var isCollision =
            VacationService.ValidateCollisionWithOtherEmployees(vacationRange, employeeVacationList, config);
        if (isCollision) continue;

        for (var vacationDate = vacationRange.StartDateTime; vacationDate < vacationRange.EndDateTime; vacationDate = vacationDate.AddDays(1))
        {
            employeeVacation.vacations.Add(vacationDate);
        }

        countVacation += config.VacationStep;
    }


    Console.WriteLine($"Дни отпуска {employeeVacation.Employee.FirstName} {employeeVacation.Employee.LastName} {employeeVacation.Employee.SecondName} : ");

    foreach (var item in employeeVacation.vacations)
    {
        Console.WriteLine(item.ToString("d"));
    }
}
Console.ReadLine();
