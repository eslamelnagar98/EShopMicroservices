namespace Discount.gRPC.Data;
public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;
    public DiscountContext(DbContextOptions<DiscountContext> options)
       : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>()
                    .HasData(Coupon.InMemoryCoupons());
    }
}
