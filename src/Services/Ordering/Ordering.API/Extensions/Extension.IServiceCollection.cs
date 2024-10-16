﻿namespace Ordering.API.Extensions;
public partial class Extension
{
    public static IServiceCollection AddOrderingServices(this IServiceCollection services)
    {
        var orderingAssembly = typeof(Program).Assembly;

        services
            .AddExceptionHandler<CustomExceptionHandler>()
            .AddCarterWithAssemblies(orderingAssembly)
            .AddTransient(typeof(IMediatrLogger<,>), typeof(MediatrLogger<,>))
            .AddMediatorBehaviors(GetOrderingAssemblies())
            .AddValidatorsFromAssemblies(GetOrderingAssemblies(), includeInternalTypes: true)
            .AddFluentValidationClientsideAdapters()
            .AddOrderingHealthChecks()
            .AddMessageBrokerWithConsumers(Assembly.Load("Ordering.Application"))
            .AddFeatureManagement();
        ;

        return services;
    }

    private static IServiceCollection AddOrderingHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
                .AddSqlServer((serviceProvider) => serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>()?.Value.ConnectionString);
        return services;
    }


    private static Assembly[] GetOrderingAssemblies()
    {
        return
        [
            Assembly.Load("Ordering.Api"),
            Assembly.Load("Ordering.Application"),
            Assembly.Load("Ordering.Infrastructure"),
            Assembly.Load("Ordering.Domain")
        ];
    }
}