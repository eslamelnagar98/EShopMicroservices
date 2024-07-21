namespace Basket.API.Jobs;
public interface IProcessOutboxMessagesJob
{
    Task ProcessMessageAsync(CancellationToken stoppingToken);
}
