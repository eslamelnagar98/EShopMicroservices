namespace Discount.gRPC.Extensions;
public partial class Extension
{
    public static async Task<WebApplication> AddMiddlewaresAsync(this WebApplication app)
    {
        await app.UseMigrationAsync();
        app.MapGrpcService<DiscountService>();
        app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
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
