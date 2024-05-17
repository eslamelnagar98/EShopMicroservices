namespace Catalog.API.Extensions;
public static partial class Extension
{
    public static WebApplication UseCatalogMiddlewares(this WebApplication app)
    {
        app.UseExceptionHandler(_ => { })
           .UseHealthChecks("/health", new HealthCheckOptions
           {
               ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
           });
        app.MapCarter();
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
