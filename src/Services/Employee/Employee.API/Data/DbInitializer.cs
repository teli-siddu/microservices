

using Employees.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Employees.API.Data
{
    public class DbInitializer
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            SeedData(scope.ServiceProvider.GetService<EmployeeDbContext>());
        }

        private static void SeedData(EmployeeDbContext employeeDbContext)
        {
            employeeDbContext.Database.Migrate();
            if (employeeDbContext.Employees.Any())
            {
                Console.WriteLine("already have data");
                return;
            }
            List<Employee> employees = new List<Employee>
            {
                new Employee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Doe",
                    Position = "Software Engineer",
                    Department = "IT",
                    Salary = 60000.00m
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jane",
                    LastName = "Smith",
                    Position = "Project Manager",
                    Department = "IT",
                    Salary = 75000.00m
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Emily",
                    LastName = "Johnson",
                    Position = "HR Manager",
                    Department = "HR",
                    Salary = 65000.00m
                }
            };
            employeeDbContext.Employees.AddRange(employees);
            employeeDbContext.SaveChanges();
        }
    }
}
