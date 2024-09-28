namespace YarpApiGateway.Extensions;
public partial class Extension
{
    public static IServiceCollection AddSlidingWindowRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(rateLimiterOptions =>
        {
            rateLimiterOptions.AddSlidingWindowLimiter("sliding", options =>
            {
                options.Window = TimeSpan.FromSeconds(30);
                options.PermitLimit = 10;
                options.SegmentsPerWindow = 3;
            });
        });

        return services;
    }

}
