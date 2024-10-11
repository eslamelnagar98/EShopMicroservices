namespace Shopping.Web.Models.Basket;
public partial class BasketCheckoutModel
{
    public string UserName { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public decimal TotalPrice { get; set; } = default!;
}

public sealed record CheckoutBasketRequest(BasketCheckoutModel BasketCheckoutDto);
public sealed record CheckoutBasketResponse(bool IsSuccess);