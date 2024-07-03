namespace Ordering.Infrastructure.Data.Interceptors;
public class DispatchDomainEventsInterceptor(IPublisher publisher) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context)
            .GetAwaiter()
            .GetResult();

        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEvents(DbContext context)
    {
        if (context is null)
            return;

        var aggregates = context.ChangeTracker
            .Entries<IAggregate>()
            .Where(a => a.Entity.DomainEvents.Any())
            .Select(a => a.Entity)
            .ToList();

        var domainEvents = aggregates
            .SelectMany(a => a.DomainEvents)
            .ToList();


        aggregates.ForEach(a => a.ClearDomainEvents());

        await PublishDomainEventAsync(domainEvents);
    }

    private async Task PublishDomainEventAsync(List<IDomainEvent> domainEvents)
    {
        await domainEvents.ForEachAsync(async domainEvent => await publisher.Publish(domainEvent));
    }
}
