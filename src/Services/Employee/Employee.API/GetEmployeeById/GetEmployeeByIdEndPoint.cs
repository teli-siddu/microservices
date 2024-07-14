
namespace Employees.API.GetEmployeeById
{
    public record GetEmployeeByIdRequest(Guid Id);
    public record GetEmployeeByIdResponse(Employee Employee);
    public class GetEmployeeByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/employees/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetEmployeeByIdQuery(id));
                var response = result.Adapt<GetEmployeeByIdResponse>();
                return Results.Ok(response);
            }).WithName("GetEmployeeById")
            .Produces<GetEmployeeByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Employee By Id")
            .WithDescription("Get Employee By Id");


        }
    }
}
