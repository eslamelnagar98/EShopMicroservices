namespace Discount.gRPC.Extensions;
public static class Registration
{
    public static async Task RunBasketAsync(this WebApplicationBuilder builder)
    {
        var logger = default(Logger);
        var serviceName = string.Empty;
        var app = default(WebApplication);
        try
        {
            logger = Extension.GetLogger();
            serviceName = Extension.GetServiceName();
            app = builder.Build();
            logger.Info($"Service {serviceName} Starts Successfully");
            await app.AddMiddlewaresAsync();
            await app.RunAsync();
        }
        catch (Exception exception)
        {
            logger?.Error(exception, $"Service {serviceName} Stopping Due To Exception");
            await app?.StopAsync();
        }
        finally
        {
            LogManager.Shutdown();
        }
    }
}
