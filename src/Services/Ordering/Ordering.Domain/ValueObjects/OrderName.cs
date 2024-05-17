namespace Ordering.Domain.ValueObjects;
public sealed record OrderName
{
    private const int DefaultLength = 5;
    public string Value { get; }
    private OrderName(string value) => Value = value;

    public static implicit operator string(OrderName orderName) => orderName.Value;

    public static implicit operator OrderName(string value) => Of(value);
    private static OrderName Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, DefaultLength);

        return new OrderName(value);
    }
}
