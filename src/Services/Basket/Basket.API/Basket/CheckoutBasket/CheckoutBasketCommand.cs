namespace Basket.API.Basket.CheckoutBasket;
internal sealed record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto)
    : ICommand<CheckoutBasketResult>;
internal sealed record CheckoutBasketResult(bool IsSuccess);
