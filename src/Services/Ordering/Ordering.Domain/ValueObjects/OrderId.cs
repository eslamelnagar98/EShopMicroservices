namespace Ordering.Domain.ValueObjects;
public sealed record OrderId
{
    public Guid Value { get; }
    private OrderId(Guid value) => Value = value;

    public static implicit operator Guid(OrderId orderId) => orderId.Value;

    public static implicit operator OrderId(Guid value) => Of(value);
    private static OrderId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("OrderId cannot be empty.");
        }

        return new OrderId(value);
    }
}
