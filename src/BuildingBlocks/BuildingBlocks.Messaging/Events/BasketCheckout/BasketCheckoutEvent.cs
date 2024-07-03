namespace BuildingBlocks.Messaging.Events.BasketCheckout;
public partial record BasketCheckoutEvent : IntegrationEvent
{
    public string UserName { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public decimal TotalPrice { get; set; } 
}

