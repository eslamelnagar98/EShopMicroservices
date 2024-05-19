namespace Ordering.Infrastructure.Data.Configurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasConversion(orderId => orderId.Value, databaseOrderId => databaseOrderId);

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        ConfigureOrderName(builder);
        ConfigureAddress(builder, o => o.ShippingAddress);
        ConfigureAddress(builder, o => o.BillingAddress);
        ConfigurePayment(builder);

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                s => s.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        builder.Property(o => o.LastModifiedBy)
            .HasMaxLength(100);

        builder.Property(o => o.CreatedBy)
            .HasMaxLength(100);

        builder.Property(o => o.TotalPrice);
    }

    private void ConfigureOrderName(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsOne(
            o => o.OrderName, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
            });
    }

    private void ConfigureAddress<TEntity>(EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, Address>> addressExpression) where TEntity : class
    {
        builder.OwnsOne(
            addressExpression, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(a => a.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(a => a.EmailAddress)
                    .HasMaxLength(50);

                addressBuilder.Property(a => a.AddressLine)
                    .HasMaxLength(180)
                    .IsRequired();

                addressBuilder.Property(a => a.Country)
                    .HasMaxLength(50);

                addressBuilder.Property(a => a.State)
                    .HasMaxLength(50);

                addressBuilder.Property(a => a.ZipCode)
                    .HasMaxLength(5)
                    .IsRequired();

                addressBuilder.WithOwner();
            });

        builder.Navigation(addressExpression).IsRequired();
    }

    private void ConfigurePayment(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsOne(
            o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardName)
                    .HasMaxLength(50);

                paymentBuilder.Property(p => p.CardNumber)
                    .HasMaxLength(24)
                    .IsRequired();

                paymentBuilder.Property(p => p.Expiration)
                    .HasMaxLength(10);

                paymentBuilder.Property(p => p.CVV)
                    .HasMaxLength(3);

                paymentBuilder.Property(p => p.PaymentMethod);
            });
    }
}
