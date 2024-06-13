using FluentValidation;
using Test.API.Commands;

namespace Test.API.Validators
{
    public class InsertPersonValidator:AbstractValidator<InsertPersonCommand>
    {
        public InsertPersonValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty()
                .WithMessage("First Name is required field");

            RuleFor(p => p.FirstName)
                .MaximumLength(10)
                .WithMessage("First Name should not exceed 10 character");

        }
    }
}
