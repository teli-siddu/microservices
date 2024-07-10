
using Employees.API.Models;

namespace Employees.API.GetEmployees
{
    public record GetEmployeesRequest(int? PageNumber=1, int? PaginationSize=10);
    public record GetEmployeesResponse(IEnumerable<Employee> Employees);
    public class GetEmployeesEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/employees", async ([AsParameters] GetEmployeesRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetEmployeesQuery>();
                var result = await sender.Send(query);
                var response = result.Adapt<GetEmployeesResponse>();
                return Results.Ok(response);
            })
            .WithName("GetEmployees")
            .Produces<GetEmployeesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Employees")
            .WithDescription("Get Employees");
        }
    }
}
