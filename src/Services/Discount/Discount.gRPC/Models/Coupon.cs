namespace Discount.gRPC.Models;
public class Coupon
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Amount { get; set; }
    public static Coupon Empty() => new() { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
    public static RpcException InvalidCouponObject() => new(new Status(StatusCode.InvalidArgument, "Invalid request object."));
    public static RpcException NotFoundCoupon(string productName)=> new (new Status(StatusCode.NotFound, $"Discount with ProductName={productName} is not found."));
    public static Coupon[] InMemoryCoupons()
    {
        return [new Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
                new Coupon { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100 }];
    }
}
