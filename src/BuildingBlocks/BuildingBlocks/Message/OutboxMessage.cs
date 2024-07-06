namespace BuildingBlocks.Message;
public class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; }

    public static OutboxMessage Create<T>(string messageType, T @event)
    {
        var payload = JsonConvert.SerializeObject(@event);

        return new()
        {
            Id = Guid.NewGuid(),
            Type = messageType,
            Payload = payload,
            CreatedAt = DateTime.UtcNow,
            IsPublished = false
        };
    }

    public void MarkAsPublished()
    {
        IsPublished = true;
        PublishedAt = DateTime.UtcNow;
    }
}
