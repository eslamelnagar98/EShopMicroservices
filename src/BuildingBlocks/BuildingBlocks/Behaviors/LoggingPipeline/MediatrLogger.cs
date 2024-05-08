namespace BuildingBlocks.Behaviors.LoggingPipeline;
public sealed class MediatrLogger<TRequest, TResponse>(ILogger<MediatrLogger<TRequest, TResponse>> logger) 
    : IMediatrLogger<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    private int MAX_ALLOWED_REQUEST_PROCESSING_TIME = 3;

    private readonly Stopwatch _timer = new();
    public void LogRequest(TRequest request)
    {
        logger.LogInformation("[START] Handle request={Request} - Response={Response} - RequestData={RequestData}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);
        _timer.Start();
    }

    public void LogResponse(TResponse response)
    {
        _timer.Stop();
        var timeTaken = _timer.Elapsed;
        if (timeTaken.Seconds > MAX_ALLOWED_REQUEST_PROCESSING_TIME)
        {
            logger.LogWarning("[PERFORMANCE] The request {Request} took {TimeTaken} seconds.",
                typeof(TRequest).Name, timeTaken.Seconds);
        }

        logger.LogInformation("[END] Handled {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name);
    }

}



