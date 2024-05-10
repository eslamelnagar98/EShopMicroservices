namespace Basket.API.Extensions;
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

    public static IServiceCollection AddBasketServices(this IServiceCollection services)
    {
        var basketAssembly = typeof(Program).Assembly;
        services
           .AddCarterWithAssemblies(basketAssembly)
           .AddTransient(typeof(IMediatrLogger<,>), typeof(MediatrLogger<,>))
           .AddScoped<IBasketRepository, BasketRepository>()
           //.Decorate<IBasketRepository, CachedBasketRepository>()
           .AddMediatorBehaviors(basketAssembly)
           .AddValidatorsFromAssembly(basketAssembly, includeInternalTypes: true)
           .AddMartenContext()
           .AddExceptionHandler<CustomExceptionHandler>()
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
            options.Schema.For<ShoppingCart>()
                          .Identity(x => x.UserName);
            return options;
        }).UseLightweightSessions();
        return services;
    }
}
