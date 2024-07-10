using BuildingBlocks.CQRS;
using Employees.API.Data;
using Employees.API.Models;
using FluentValidation;

namespace Employees.API.CreateEmployee
{
    public record CreateEmployeeCommand(string FirstName, string LastName, string Position, string Department, decimal Salary)
        :ICommand<CreateEmployeeResult>;

    public record CreateEmployeeResult(Guid Id);

    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required");
            RuleFor(x => x.Department).NotEmpty().WithMessage("Department Name is required");
            RuleFor(x => x.Salary).GreaterThan(0).WithMessage("Salary must be greater than zero");
        }
    }
    internal class CreateEmployeeCommandHandler
        (EmployeeDbContext _dbContext) 
        : ICommandHandler<CreateEmployeeCommand, CreateEmployeeResult>
    {
        public async Task<CreateEmployeeResult> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee=   request.Adapt<Employee>();
            await _dbContext.AddAsync<Employee>(employee);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(new CreateEmployeeResult(employee.Id));
        
        }
    }
}
