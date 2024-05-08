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
        (var catalogAssembly, var assemblies) = GetAssemblies();
        services
           .AddCarter(new DependencyContextAssemblyCatalog(assemblies))
           .AddTransient(typeof(IMediatrLogger<,>), typeof(MediatrLogger<,>))
           .AddCatalogMediatR(catalogAssembly)
           .AddValidatorsFromAssembly(catalogAssembly)
           .AddMartenContext()
           .AddExceptionHandler<CustomExceptionHandler>()
           .AddHealthChecks()
           .AddNpgSql((serviceProvider) => serviceProvider.GetRequiredService<IOptions<DatabaseSettingsOptions>>()?.Value.ConnectionString)
           ;
        return services;
    }

    public static IServiceCollection AddCatalogOptions<TOptions>(this IServiceCollection services, string sectionName)
        where TOptions : class
    {
        services
            .AddOptionsWithValidateOnStart<TOptions>()
            .Configure<IConfiguration>(
            (options, configuration) =>
             configuration.GetSection(sectionName)
            .Bind(options))
            .ValidateFluently();
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
        }).UseLightweightSessions();
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

    public static OptionsBuilder<TOptions> ValidateFluently<TOptions>(this OptionsBuilder<TOptions> optionsBuilder) where TOptions : class
    {
        optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(servciceProvider =>
        {
            using var scope = servciceProvider.CreateScope();
            return new FluentValidationOptions<TOptions>(scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>());
        });
        return optionsBuilder;
    }
}
