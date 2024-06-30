using Logger = NLog.Logger;

namespace Ordering.API.Extensions;

internal static class Registration
{
    public static async Task RunOrderingAsync(this WebApplicationBuilder builder)
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
            await app.UseOrderingMiddlewaresAsync();
            await app.RunAsync();
        }
        catch (Exception exception)
        {
            logger?.Error(exception, $"Service {serviceName} Stopping Due To Exception");
            await app?.StopAsync()!;
        }
        finally
        {
            LogManager.Shutdown();
        }
    }
}