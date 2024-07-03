namespace BuildingBlocks.Messaging.Options;
public class MessageBrokerOptions
{
    public const string SectionName = "MessageBroker";
    public string Host { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

}
