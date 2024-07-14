using BuildingBlocks.Pagination;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Employees.API.GetEmployees
{

    public record GetEmployeesQuery(PaginationRequest PaginationRequest) :IQuery<GetEmployeesResult>;

    public record GetEmployeesResult(PaginatedResult<Employee> Employees);
    public class GetEmployeesHandler(EmployeeDbContext _dbContext)
        : IQueryHandler<GetEmployeesQuery, GetEmployeesResult>
    {
        public async Task<GetEmployeesResult> Handle(GetEmployeesQuery query, CancellationToken cancellationToken)
        {

            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;
            var totalCount = await _dbContext.Employees.LongCountAsync(cancellationToken);
            var employees= await _dbContext.Employees
                .OrderBy(x=>x.FirstName)
                .Skip(pageIndex *pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
            return new GetEmployeesResult(new PaginatedResult<Employee>(pageIndex,pageSize,totalCount,employees));
        }
    }
}
