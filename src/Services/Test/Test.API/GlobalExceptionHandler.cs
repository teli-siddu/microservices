using Microsoft.AspNetCore.Diagnostics;
using Test.API.Exceptions;

namespace Test.API
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailsService;

        public GlobalExceptionHandler(IProblemDetailsService problemDetailsService)
        {
            this._problemDetailsService = problemDetailsService;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {


            httpContext.Response.ContentType = "application/json";
            var exDetails = exception switch
            {
                ValidationAppException=>(Detail:exception.Message, StatusCode:StatusCodes.Status422UnprocessableEntity),
                _ => (Detail: exception.Message, StatusCode: StatusCodes.Status500InternalServerError)
            };

            httpContext.Response.StatusCode = exDetails.StatusCode;
            if(exception is ValidationAppException validationAppException)
            {
                await httpContext.Response.WriteAsJsonAsync(new { validationAppException.Errors });
                return true;
            }

            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails =
                {
                    Title="An Error Occured",
                    Detail=exDetails.Detail,
                    Type=exception.GetType().Name,
                    Status=exDetails.StatusCode
                },
                Exception = exception
            });
        }
    }
}
