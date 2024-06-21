namespace Ordering.Application.Exceptions;
public class OrderNotFoundException : BuildingBlocks.Exceptions.NotFoundException
{
    private OrderNotFoundException(string? paramName = "Order", Guid id = default) : base(paramName, id) { }

    public static void ThrowIfNull([NotNull] Order order, Guid orderId, [CallerArgumentExpression("order")] string? paramName = null)
    {
        if (order is null)
        {
            throw new OrderNotFoundException(paramName: paramName, id: orderId);
        }
    }
}

