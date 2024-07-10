using BuildingBlocks.CQRS;
using Employees.API.Data;
using Employees.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Employees.API.GetEmployees
{

    public record GetEmployeesQuery(int? PageNumber = 1, int? PaginationSize = 10) :IQuery<GetEmployeesResult>;

    public record GetEmployeesResult(IEnumerable<Employee> Employees);
    public class GetEmployeesHandler(EmployeeDbContext _dbContext)
        : IQueryHandler<GetEmployeesQuery, GetEmployeesResult>
    {
        public async Task<GetEmployeesResult> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            int pageNumber = request.PageNumber ?? 1;
            int pageSize = request.PaginationSize ?? 10;
            var employees= await _dbContext.Employees
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
            return new GetEmployeesResult(employees);
        }
    }
}
