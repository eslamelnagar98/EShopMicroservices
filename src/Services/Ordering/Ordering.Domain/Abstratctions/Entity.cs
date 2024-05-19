namespace Ordering.Domain.Abstratctions;
public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; set; }
    public DateTime? CreatedAt { get; set; } = default!;
    public string? CreatedBy { get; set; }=string.Empty;
    public DateTime? LastModified { get; set; } = default!;
    public string? LastModifiedBy { get; set; } = string.Empty;
}
