namespace Ordering.Infrastructure.Data.Configurations;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(productId => productId.Value, databaseProductId => databaseProductId);

        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(o => o.LastModifiedBy)
           .HasMaxLength(100);

        builder.Property(o => o.CreatedBy)
            .HasMaxLength(100);
    }
}
