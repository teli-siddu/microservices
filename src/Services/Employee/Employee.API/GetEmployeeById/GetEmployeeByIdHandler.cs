
namespace Employees.API.GetEmployeeById
{

    public record GetEmployeeByIdQuery(Guid Id):IQuery<GetEmployeeByIdResult>;

    public record GetEmployeeByIdResult(Employee Employee);
    public class GetEmployeeByIdHandler(EmployeeDbContext _dbContext) : IQueryHandler<GetEmployeeByIdQuery, GetEmployeeByIdResult>
    {
        public async Task<GetEmployeeByIdResult> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == request.Id);

            if(employee is null)
            {
                throw new EmployeeNotFoundException(request.Id);
            }
            return new GetEmployeeByIdResult(employee);
        }
    }
}
