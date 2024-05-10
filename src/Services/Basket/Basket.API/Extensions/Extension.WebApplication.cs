namespace Basket.API.Extensions;
public static partial class Extension
{
    public static WebApplication AddMiddlewares(this WebApplication app)
    {
        app.MapCarter();
        app.UseExceptionHandler(options => { })
           .UseHealthChecks("/health", new HealthCheckOptions
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
}
