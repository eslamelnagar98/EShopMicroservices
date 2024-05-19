namespace Ordering.API.Extensions;
public static partial class Extension
{
    public static async Task<WebApplication> UseOrderingMiddlewaresAsync(this WebApplication app)
    {
        app.UseExceptionHandler(_ => { });
        await app.TryAddInitializeSqlWith();
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
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

    private static async Task<WebApplication> TryAddInitializeSqlWith(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            await app.InitialiseDatabaseAsync();
        }
        return app;
    }
}
