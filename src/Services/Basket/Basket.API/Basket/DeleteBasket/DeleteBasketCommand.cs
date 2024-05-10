namespace Basket.API.Basket.DeleteBasket;
internal sealed record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
internal sealed record DeleteBasketResult(bool IsSuccess);
