
using BuildingBlocks.Pagination;
using Employees.API.Models;

namespace Employees.API.GetEmployees
{
    //public record GetEmployeesRequest(PaginationRequest PaginationRequest);
    public record GetEmployeesResponse(PaginatedResult<Employee> Employees);
    public class GetEmployeesEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/employees", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetEmployeesQuery(request));
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
