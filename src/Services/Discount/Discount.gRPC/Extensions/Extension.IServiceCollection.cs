﻿namespace Discount.gRPC.Extensions;
public static partial class Extension
{
    public static IServiceCollection AddDiscountServices(this IServiceCollection services)
    {
        var catalogAssembly = typeof(Program).Assembly;
        services 
           .AddExceptionHandler<CustomExceptionHandler>()
           .AddValidatorsFromAssembly(catalogAssembly, includeInternalTypes: true)
           .AddDiscountDbContext()
           .AddGrpc()
           ;
        return services;
    }
    private static IServiceCollection AddDiscountDbContext(this IServiceCollection services)
    {
        return services.AddDbContext<DiscountContext>((serviceProvider, options) =>
        {
            var connectionString = serviceProvider.GetRequiredService<IOptions<DatabaseSettingsOptions>>()?.Value.ConnectionString;

            options.UseSqlite(connectionString);
        });
    }
}
