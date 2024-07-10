using Employees.API.Models;
using Microsoft.EntityFrameworkCore;
namespace Employees.API.Data
{
    public class EmployeeDbContext:DbContext
    {

        public EmployeeDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
