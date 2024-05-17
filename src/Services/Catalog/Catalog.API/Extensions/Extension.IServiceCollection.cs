namespace Catalog.API.Extensions;
public partial class Extension
{
    public static IServiceCollection TryAddInitializeMartenWith<TInitialData>(this WebApplicationBuilder builder) where TInitialData : class, IInitialData
    {
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.InitializeMartenWith<TInitialData>();
        }
        return builder.Services;
    }

    public static IServiceCollection AddCatalogServices(this IServiceCollection services)
    {
        var catalogAssembly = typeof(Program).Assembly;
        services
           .AddExceptionHandler<CustomExceptionHandler>()
           .AddCarterWithAssemblies(catalogAssembly)
           .AddTransient(typeof(IMediatrLogger<,>), typeof(MediatrLogger<,>))
           .AddMediatorBehaviors(catalogAssembly)
           .AddValidatorsFromAssembly(catalogAssembly, includeInternalTypes: true)
           .AddMartenContext()
           .AddHealthChecks()
           .AddNpgSql((serviceProvider) => serviceProvider.GetRequiredService<IOptions<DatabaseSettingsOptions>>()?.Value.ConnectionString)
           ;
        return services;
    }
    private static IServiceCollection AddMartenContext(this IServiceCollection services)
    {
        services.AddMarten(serviceProvider =>
        {
            var connectionString = serviceProvider.GetRequiredService<IOptions<DatabaseSettingsOptions>>()?.Value?.ConnectionString;
            var options = new StoreOptions();
            options.Connection(connectionString);
            options.DisableNpgsqlLogging = true;
            return options;
        }).UseLightweightSessions();
        return services;
    }
}
