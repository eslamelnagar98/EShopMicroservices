namespace Ordering.Domain.ValueObjects;
public class OrderItemId
{
    public Guid Value { get; }
    private OrderItemId(Guid value) => Value = value;

    public static implicit operator Guid(OrderItemId orderitemId) => orderitemId.Value;

    public static implicit operator OrderItemId(Guid value) => Of(value);
    private static OrderItemId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("OrderItemId cannot be empty.");
        }

        return new OrderItemId(value);
    }
}
