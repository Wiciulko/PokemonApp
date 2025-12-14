using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Exceptions
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails
            {
                Title = exception.Message,
                Detail = exception.GetType().Name,
            };

            switch (exception)
            {
                case InternalServerException:
                    problemDetails.Status = context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
                case ValidationException:
                    problemDetails.Status = context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case BadRequestException:
                    problemDetails.Status = context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case NotFoundException:
                    problemDetails.Status = context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                default:
                    problemDetails.Status = context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            problemDetails.Instance = context.Request.Path;
            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

            if (exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
            }

            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
