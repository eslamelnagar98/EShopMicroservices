namespace Basket.API.Jobs;
public class OutboxProcessorJob(IServiceProvider serviceProvider, ILogger<OutboxProcessorJob> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IBasketDbRepository>();

                var publisher = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                var outboxMessages = await GetUnpublishedOutboxMessages(repository, stoppingToken);

                await outboxMessages.ForEachAsync(async message => await ProcessMessage(message, repository, publisher, stoppingToken));

                await Task.Delay(5000, stoppingToken);
            }
        }
    }

    private async Task<IReadOnlyList<OutboxMessage>> GetUnpublishedOutboxMessages(IBasketDbRepository repository, CancellationToken stoppingToken)
    {
        return await repository.GetAllDataAsync<OutboxMessage>(message => !message.IsPublished, stoppingToken);
    }

    private async Task ProcessMessage(OutboxMessage message, IBasketDbRepository repository, IPublishEndpoint publishEndpoint, CancellationToken stoppingToken)
    {
        var eventType = GetEventType(message.Type);

        if (eventType is null)
        {
            return;
        }

        var @event = DeserializeEvent(message.Payload, eventType);

        await PublishEvent(@event, publishEndpoint, stoppingToken);

        await MarkMessageAsPublished(message, repository, stoppingToken);
    }

    private Type GetEventType(string messageType)
    {
        var eventType = Type.GetType(messageType);
        if (eventType == null)
        {
            logger.LogWarning("Event type not found: {EventType}", messageType);
            throw new Exception($"Event type not found: {messageType}");
        }

        return eventType;
    }

    private object DeserializeEvent(string payload, Type eventType)
    {
        return JsonConvert.DeserializeObject(payload, eventType);
    }

    private async Task PublishEvent(object @event, IPublishEndpoint publishEndpoint, CancellationToken stoppingToken)
    {
        if (@event == null)
        {
            throw new Exception("Event is null");
        }

        await publishEndpoint.Publish(@event, stoppingToken);
    }

    private async Task MarkMessageAsPublished(OutboxMessage message, IBasketDbRepository repository, CancellationToken stoppingToken)
    {
        message.MarkAsPublished();

        await repository.StoreEntity(message, stoppingToken);

        logger.LogInformation("Successfully published outbox message: {MessageId}", message.Id);
    }
}
