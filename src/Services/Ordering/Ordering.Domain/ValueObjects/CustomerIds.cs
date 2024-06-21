namespace Ordering.Domain.ValueObjects;
public sealed record CustomerId
{
    public Guid Value { get; set; }
    private CustomerId(Guid value) => Value = value;

    public static implicit operator Guid(CustomerId customerId) => customerId.Value;

    public static implicit operator CustomerId(Guid value) => Of(value);

    private static CustomerId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value == Guid.Empty)
        {
            throw new DomainException("CustomerId cannot be empty.");
        }

        return new CustomerId(value);
    }
}

