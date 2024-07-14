namespace Employees.API.CreateEmployee;

public record CreateEmployeeRequest(string FirstName, string LastName, string Position, string Department, decimal Salary);
public record CreateEmployeeResponse(Guid Id);
public class CreateEmployeeEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/employees", async (CreateEmployeeRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateEmployeeCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateEmployeeResponse>();
            return Results.Created($"/api/employees/{response.Id}", response);
        })
        .WithName("CreateEmployee")
        .Produces<CreateEmployeeResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Emplyee Created")
        .WithDescription("Emplyee Created");
    }
}

