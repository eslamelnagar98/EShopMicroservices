namespace Ordering.Application.Orders.Command.UpdateOrder;
public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

