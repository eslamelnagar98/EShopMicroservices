namespace Basket.API.Data.Interceptors;
public class OutboxSessionListener : IDocumentSessionListener
{
    private readonly List<OutboxMessage> _outboxMessages = new();
    public void BeforeSaveChanges(IDocumentSession session)
    {
        foreach (var outboxMessage in _outboxMessages)
        {
            session.Store(outboxMessage);
        }
    }
    public void AfterCommit(IDocumentSession session, IChangeSet commit) { }
    public void DocumentLoaded(object id, object document) { }
    public void DocumentAddedForStorage(object id, object document) { }
    public Task AfterCommitAsync(IDocumentSession session, IChangeSet commit, CancellationToken token) => Task.CompletedTask;
    public Task BeforeSaveChangesAsync(IDocumentSession session, CancellationToken token) => Task.CompletedTask;
    public void AddOutboxMessage<T>(string messageType, T @event)
    {
        var payload = JsonConvert.SerializeObject(@event);

        _outboxMessages.Add(new OutboxMessage
        {
            Id = Guid.NewGuid(),
            Type = messageType,
            Payload = payload,
            CreatedAt = DateTime.UtcNow,
            IsPublished = false
        });
    }
}
