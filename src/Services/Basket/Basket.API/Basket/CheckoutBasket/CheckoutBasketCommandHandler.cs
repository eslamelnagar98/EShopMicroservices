namespace Basket.API.Basket.CheckoutBasket;
internal class CheckoutBasketCommandHandler(IBasketRepository repository, IBasketDbRepository basketDbRepository)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);

        if (basket is null)
        {
            return new CheckoutBasketResult(false);
        }

        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();

        eventMessage.TotalPrice = basket.TotalPrice;

        var outboxMessage = OutboxMessage.Create(eventMessage.GetType().FullName, eventMessage);

        basketDbRepository.StoreOutboxMessage(outboxMessage);

        await repository.DeleteBasket(command.BasketCheckoutDto.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }

}