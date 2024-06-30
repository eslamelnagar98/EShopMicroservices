namespace Ordering.Application.Orders.Command.CreateOrder;
public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

 