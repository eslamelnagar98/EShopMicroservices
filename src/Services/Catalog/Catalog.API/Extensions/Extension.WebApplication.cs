using System.Reflection;
namespace Catalog.API.Configurations;
public static partial class Extension
{
    public static WebApplication AddMiddlewares(this WebApplication app)
    {
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
