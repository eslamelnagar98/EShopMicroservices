namespace Basket.API.Dtos.BasketCheckout;
public sealed partial class BasketCheckoutDto
{
    public string UserName { get; set; } = string.Empty;
    public Guid CustomerId { get; set; } 
    public decimal TotalPrice { get; set; }
}
