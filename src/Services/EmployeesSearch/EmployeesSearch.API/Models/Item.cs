using MongoDB.Entities;

namespace EmployeesSearch.API.Models
{
    public class Item:Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Position { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
    }
}
