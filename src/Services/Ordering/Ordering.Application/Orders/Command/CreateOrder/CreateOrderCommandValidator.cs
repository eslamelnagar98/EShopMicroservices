namespace Ordering.Application.Orders.Command.CreateOrder;
public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order.OrderName)
            .NotEmptyMessage();

        RuleFor(x => x.Order.CustomerId)
            .NotNullMessage();

        RuleFor(x => x.Order.OrderItems)
            .NotEmptyMessage();
    }
}
