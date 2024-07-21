namespace Basket.API.Extensions;
public static partial class Extension
{
    public static WebApplication UseBasketMiddlewares(this WebApplication app)
    {
        app.UseExceptionHandler(_ => { })
           .UseHealthChecks("/health", new HealthCheckOptions
           {
               ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
           });
        app.MapCarter();
        app.UseHangfireDashboardMonitor();
        app.UseBackgroundJobs();
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

    private static IApplicationBuilder UseHangfireDashboardMonitor(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseHangfireDashboard(options: new DashboardOptions
            {
                Authorization = [],
                DarkModeEnabled = false,
            });
        }

        return app;
    }

    private static IApplicationBuilder UseBackgroundJobs(this WebApplication app)
    {
        var cronSetting = app.Services.GetRequiredService<IOptions<PersistenceSettingsOptions>>()?.Value;
        app.Services
            .GetRequiredService<IRecurringJobManager>()
            .AddOrUpdate<IProcessOutboxMessagesJob>(
            "outbox-processor",
            job => job.ProcessMessageAsync(CancellationToken.None), cronSetting.Cron);

        return app;

    }
}
