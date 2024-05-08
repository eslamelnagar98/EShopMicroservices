namespace BuildingBlocks.Exceptions.Handler;
public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(
            "Error Message: {exceptionMessage}, Time of occurrence {time}",
            exception.Message, DateTime.UtcNow);
        var message = exception.Message;
        var typeName = exception.GetType().Name;
        (string detail, string title, int statusCode) details = exception switch
        {
            InternalServerException => InternalServerException.Generate(message, typeName, context),
            ValidationException 
            or BadRequestException 
            or BadHttpRequestException => BadRequestException.Generate(message, typeName, context),
            NotFoundException => NotFoundException.Generate(message, typeName, context),
            _ => InternalServerException.Generate(message, typeName, context)
        };

        var problemDetails = CreateProblemDetails(details, context.Request.Path);

        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
        }

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        return true;
    }
    private ProblemDetails CreateProblemDetails((string detail, string title, int statusCode) details, PathString path)
    {
        return new()
        {
            Title = details.title,
            Detail = details.detail,
            Status = details.statusCode,
            Instance = path
        };
    }
}
