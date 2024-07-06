namespace Basket.API.Extensions;
public partial class Extension
{
    public static IServiceCollection TryAddInitializeMartenWith<TInitialData>(this WebApplicationBuilder builder) where TInitialData : class, IInitialData
    {
        return builder.Environment.IsDevelopment()
            ? builder.Services.InitializeMartenWith<TInitialData>()
            : builder.Services;
    }

    public static IServiceCollection AddBasketServices(this IServiceCollection services)
    {
        var basketAssembly = typeof(Program).Assembly;
        services
           .AddExceptionHandler<CustomExceptionHandler>()
           .AddCarterWithAssemblies(basketAssembly)
           .AddTransient(typeof(IMediatrLogger<,>), typeof(MediatrLogger<,>))
           .AddPersistence()
           .AddMediatorBehaviors(basketAssembly)
           .AddValidatorsFromAssembly(basketAssembly, includeInternalTypes: true)
           .AddMartenContext()
           .AddBasketHealthChecks()
           .AddDiscountGrpcClient()
           .AddMessageBroker()
           .AddRedisConnection()
           ;
        return services;
    }


    private static IServiceCollection AddMartenContext(this IServiceCollection services)
    {
        services.AddMarten(serviceProvider =>
        {
            var connectionString = serviceProvider.GetRequiredService<IOptions<PersistenceSettingsOptions>>()?.Value?.ConnectionString;

            var options = new StoreOptions();
            options.Connection(connectionString);
            options.DisableNpgsqlLogging = true;
            options.Schema.For<ShoppingCart>()
                          .Identity(x => x.UserName);

            return options;
        }).UseLightweightSessions();
        return services;
    }

    private static IServiceCollection AddRedisConnection(this IServiceCollection services)
    {
        return services.AddStackExchangeRedisCache(options =>
        {
            var redisConnection = services.BuildServiceProvider()
                                          .GetRequiredService<IOptions<PersistenceSettingsOptions>>()?.Value?.Redis;
            options.Configuration = redisConnection;
        });
    }

    private static IServiceCollection AddBasketHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
                .AddNpgSql((serviceProvider) => serviceProvider.GetRequiredService<IOptions<PersistenceSettingsOptions>>()?.Value.ConnectionString)
                .AddRedis((serviceProvider) => serviceProvider.GetRequiredService<IOptions<PersistenceSettingsOptions>>()?.Value.Redis)
                ;

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IBasketRepository, BasketRepository>()
                .Decorate<IBasketRepository, CachedBasketRepository>();

        return services.AddScoped<IBasketDbRepository, BasketDbRepository>();
    }

    private static IServiceCollection AddDiscountGrpcClient(this IServiceCollection services)
    {
        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>((serviceProvider, options) =>
        {
            var discountUrl = serviceProvider.GetRequiredService<IOptions<GrpcSettingsOptions>>()?.Value.DiscountUrl;
            options.Address = new Uri(discountUrl);
        }).ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            return handler;
        });
        return services;
    }
}

