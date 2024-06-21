namespace Ordering.Application.Orders.Command.DeleteOrder;
public class DeleteOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = (OrderId)command.OrderId;

        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);

        OrderNotFoundException.ThrowIfNull(order, command.OrderId);

        await SaveDeleteOrderChangesAsync(order, cancellationToken);

        return new DeleteOrderResult(true);
    }

    private async Task SaveDeleteOrderChangesAsync(Order order, CancellationToken cancellationToken)
    {
        dbContext.Orders.Update(order);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}

