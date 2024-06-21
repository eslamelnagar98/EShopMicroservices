namespace Ordering.Application.Orders.Command.CreateOrder;
public class UpdateOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.Order);

        await SaveAddOrderChangesAsync(order, cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }

    private async Task SaveAddOrderChangesAsync(Order order, CancellationToken cancellationToken)
    {
        dbContext.Orders.Add(order);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private Order CreateNewOrder(OrderDto orderDto)
    {
        var shippingAddress = CreateAddress(orderDto.ShippingAddress);

        var billingAddress = CreateAddress(orderDto.BillingAddress);

        var payment = CreatePayment(orderDto);

        var newOrder = CreateOrder(shippingAddress, billingAddress, orderDto, payment);

        foreach (var orderItemDto in orderDto.OrderItems)
        {
            newOrder.Add(orderItemDto.ProductId, orderItemDto.Quantity, orderItemDto.Price);
        }

        return newOrder;
    }

    private Address CreateAddress(AddressDto addressDto)
    {
        return Address.Of(addressDto.FirstName,
                          addressDto.LastName,
                          addressDto.EmailAddress,
                          addressDto.AddressLine,
                          addressDto.Country,
                          addressDto.State,
                          addressDto.ZipCode);
    }

    private Order CreateOrder(Address shippingAddress, Address billingAddress, OrderDto orderDto, Payment payment)
    {
        return Order.Create(id: Guid.NewGuid(),
                            customerId: orderDto.CustomerId,
                            orderName: orderDto.OrderName,
                            shippingAddress: shippingAddress,
                            billingAddress: billingAddress,
                            payment: payment);
    }

    private Payment CreatePayment(OrderDto orderDto)
    {
        return Payment.Of(orderDto.Payment.CardName,
                          orderDto.Payment.CardNumber,
                          orderDto.Payment.Expiration,
                          orderDto.Payment.Cvv,
                          orderDto.Payment.PaymentMethod);
    }

}
