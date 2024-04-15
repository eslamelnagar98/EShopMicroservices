namespace Catalog.API.Configurations;
public partial class Extension
{
    public static IServiceCollection AddCatalogServices(this IServiceCollection services)
    {
        (var catalogAssembly, var assemblies) = GetAssemblies();
        services
           .AddCarter(new DependencyContextAssemblyCatalog(assemblies))
           .AddTransient(typeof(IMediatrLogger<,>), typeof(MediatrLogger<,>))
           .AddCatalogMediatR(catalogAssembly)
           .AddValidatorsFromAssembly(catalogAssembly)
           .AddMartenContext()
        ;
        return services;
    }

    public static IServiceCollection AddCatalogOptions(this IServiceCollection services)
    {
        services
            .AddOptions<DatabaseSettingsOptions>()
            .Configure<IConfiguration>(
            (options, configuration) =>
             configuration.GetSection(DatabaseSettingsOptions.SectionName)
            .Bind(options))
            .ValidateOnStart();
        return services;
    }
    private static IServiceCollection AddMartenContext(this IServiceCollection services)
    {
        services.AddMarten(serviceProvider =>
        {
            var connectionString = serviceProvider.GetRequiredService<IOptions<DatabaseSettingsOptions>>()?.Value?.ConnectionString;
            var options = new StoreOptions();
            options.Connection(connectionString);
            return options;
        })
          .UseLightweightSessions();
        return services;
    }

    private static IServiceCollection AddCatalogMediatR(this IServiceCollection services, Assembly catalogAssembly)
    {
        return services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(catalogAssembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
    }

    private static (Assembly catalogAssembly, Assembly[] assemblies) GetAssemblies()
    {
        var catalogAssembly = typeof(Program).Assembly;
        var assemblies = AppDomain.CurrentDomain
            .GetAssemblies()
            .Where(assembly => assembly.GetName().Name == "BuildingBlocks" || assembly == catalogAssembly)
            .ToArray();

        return (catalogAssembly, assemblies);

    }




}
