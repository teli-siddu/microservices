
namespace Employees.API.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Position { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
    }
}
