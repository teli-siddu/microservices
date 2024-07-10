using BuildingBlocks.Exceptions;

namespace Employees.API.Exceptions
{
    public class EmployeeNotFoundException:NotFoundException
    {
        public EmployeeNotFoundException(Guid id):base("product",id)
        {
            
        }
    }
}
