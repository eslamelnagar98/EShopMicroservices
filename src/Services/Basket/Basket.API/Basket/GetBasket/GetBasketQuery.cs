namespace Basket.API.Basket.GetBasket;
internal sealed record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
internal sealed record GetBasketResult(ShoppingCart Cart);

