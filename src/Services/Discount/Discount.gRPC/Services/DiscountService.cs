namespace Discount.gRPC.Services;
public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
           : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await GetCoupon(request.ProductName) ?? Coupon.Empty();

        logger.LogInformation("Discount is retrieved for ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>() ?? throw Coupon.InvalidCouponObject();

        return await AddOrUpdateAsync(coupon, dbContext.Coupons.Add, "Created");
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>() ?? throw Coupon.InvalidCouponObject();

        return await AddOrUpdateAsync(coupon, dbContext.Coupons.Update, "Updated");
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await GetCoupon(request.ProductName) ?? throw Coupon.NotFoundCoupon(request.ProductName);

        dbContext.Coupons.Remove(coupon);

        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully deleted. ProductName : {ProductName}", request.ProductName);

        return new DeleteDiscountResponse { Success = true };
    }

    private async Task<Coupon> GetCoupon(string productName)
    {
        return await dbContext?.Coupons
                              ?.FirstOrDefaultAsync(x => x.ProductName == productName)
                              ;
    }

    private async Task<CouponModel> AddOrUpdateAsync(Coupon coupon, Func<Coupon, EntityEntry<Coupon>> commandFunc, string operationName)
    {
        commandFunc(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Discount is successfully {Operation}. ProductName : {ProductName}", operationName, coupon.ProductName);
        return coupon.Adapt<CouponModel>();
    }
}
