namespace BuildingBlocks.Behaviors.LoggingPipeline;
public sealed class LoggingBehavior<TRequest, TResponse>(IMediatrLogger<TRequest, TResponse> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogRequest(request);
        var response = await next();
        logger.LogResponse(response);
        return response;
    }
}
