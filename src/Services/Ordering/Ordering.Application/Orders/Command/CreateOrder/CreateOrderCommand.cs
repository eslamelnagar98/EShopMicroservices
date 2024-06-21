namespace Ordering.Application.Orders.Command.CreateOrder;
public record UpdateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

 