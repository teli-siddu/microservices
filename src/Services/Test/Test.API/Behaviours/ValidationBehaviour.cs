using FluentValidation;
using MediatR;
using Test.API.Exceptions;

namespace Test.API.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

         var errorsDict =   _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x is not null)
                .GroupBy(

                x => x.PropertyName.Substring(x.PropertyName.IndexOf('.') + 1),
                x => x.ErrorMessage, (propertyName, errorMessages) => new
                {
                    key = propertyName,
                    Values = errorMessages.Distinct().ToArray()
                }
                ).ToDictionary(x => x.key, x => x.Values);

            if (errorsDict.Any())
            {
                throw new ValidationAppException(errorsDict);
            }

            return await next();
        }
    }
}
