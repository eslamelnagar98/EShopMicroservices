namespace Ordering.API.Extensions;
public partial class Extension
{

    public static IServiceCollection AddOrderingServices(this IServiceCollection services)
    {
        var orderingAssembly = typeof(Program).Assembly;
        services
           .AddExceptionHandler<CustomExceptionHandler>()
           //.AddCarterWithAssemblies(catalogAssembly)
           .AddTransient(typeof(IMediatrLogger<,>), typeof(MediatrLogger<,>))
           .AddMediatorBehaviors(orderingAssembly)
           .AddValidatorsFromAssemblies(GetOrderingAssemblies(), includeInternalTypes: true)
           .AddFluentValidationClientsideAdapters()
           .AddOrderingHealthChecks()
           ;
        return services;
    }

    private static IServiceCollection AddOrderingHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
                .AddSqlServer((serviceProvider) => serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>()?.Value.ConnectionString)
                ;
        return services;
    }


    private static Assembly[] GetOrderingAssemblies()
    {
        return [Assembly.Load("Ordering.Api"),
            Assembly.Load("Ordering.Application"),
            Assembly.Load("Ordering.Infrastructure"),
            Assembly.Load("Ordering.Domain")];
    }
}
