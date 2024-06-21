namespace Ordering.Application.Orders.Command.UpdateOrder;
public class UpdateOrderCommandHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = (OrderId)command.Order.Id;

        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);

        OrderNotFoundException.ThrowIfNull(order, command.Order.Id);

        UpdateOrderWithNewValues(order, command.Order);

        await SaveUpdateOrderChangesAsync(order, cancellationToken);

        return new UpdateOrderResult(true);
    }

    private async Task SaveUpdateOrderChangesAsync(Order order, CancellationToken cancellationToken)
    {
        dbContext.Orders.Update(order);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
    {
        var shippingAddress = UpdateAddress(orderDto.ShippingAddress);

        var billingAddress = UpdateAddress(orderDto.BillingAddress);

        var payment = UpdatePayment(orderDto);

        order.Update(orderName: orderDto.OrderName,
                     shippingAddress: shippingAddress,
                     billingAddress: billingAddress,
                     payment: payment,
                     status: orderDto.Status);
    }

    private Address UpdateAddress(AddressDto addressDto)
    {
        return Address.Of(addressDto.FirstName,
                          addressDto.LastName,
                          addressDto.EmailAddress,
                          addressDto.AddressLine,
                          addressDto.Country,
                          addressDto.State,
                          addressDto.ZipCode);
    }

    private Payment UpdatePayment(OrderDto orderDto)
    {
        return Payment.Of(orderDto.Payment.CardName,
                          orderDto.Payment.CardNumber,
                          orderDto.Payment.Expiration,
                          orderDto.Payment.Cvv,
                          orderDto.Payment.PaymentMethod);
    }
}
