namespace BuildingBlocks.Behaviors.LoggingPipeline;
public interface IMediatrLogger<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    void LogRequest(TRequest request);
    void LogResponse(TResponse response);
}
