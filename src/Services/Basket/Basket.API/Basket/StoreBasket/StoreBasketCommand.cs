namespace Basket.API.Basket.StoreBasket;
internal sealed record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
internal sealed record StoreBasketResult(string UserName);
