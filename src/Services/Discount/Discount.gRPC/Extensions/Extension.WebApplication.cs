namespace Discount.gRPC.Extensions;
public partial class Extension
{
    public static async Task<WebApplication> UseDiscountMiddlewaresAsync(this WebApplication app)
    {
        app.UseExceptionHandler(_ => { });
        await app.UseMigrationAsync();
        app.MapGrpcService<DiscountService>();
        return app;
    }
    public static Logger GetLogger()
    {
        return LogManager
            .Setup()
            .LoadConfigurationFromAppSettings()
            .GetCurrentClassLogger();
    }
    public static string GetServiceName()
    {
        return Assembly
            .GetExecutingAssembly()
            .GetName()
            .Name;
    }

    private static async Task<WebApplication> UseMigrationAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
        await dbContext.Database.MigrateAsync();
        return app;
    }
}
