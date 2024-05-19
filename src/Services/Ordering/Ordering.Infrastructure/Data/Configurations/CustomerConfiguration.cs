namespace Ordering.Infrastructure.Data.Configurations;
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion(customerId => customerId.Value, databaseCustomerId => databaseCustomerId);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasMaxLength(255);

        builder.HasIndex(c => c.Email)
            .IsUnique();

        builder.Property(c => c.LastModifiedBy)
            .HasMaxLength(100);

        builder.Property(c => c.CreatedBy)
            .HasMaxLength(100);
    }
}
