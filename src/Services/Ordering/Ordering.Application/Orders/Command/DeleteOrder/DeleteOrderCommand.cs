namespace Ordering.Application.Orders.Command.DeleteOrder;
public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;

public record DeleteOrderResult(bool IsSuccess);
