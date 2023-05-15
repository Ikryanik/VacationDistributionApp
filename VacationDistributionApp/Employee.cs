namespace VacationDistributionApp;

public class Employee
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? SecondName { get; set; }

    public Employee(string lastName, string firstName, string? secondName)
    {
        LastName = lastName;
        FirstName = firstName;
        SecondName = secondName;
    }
}