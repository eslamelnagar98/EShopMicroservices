namespace YarpApiGateway.Extensions;
public static partial class Extension
{
    public static IServiceCollection AddReverseProxyFromConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddReverseProxy()
                .LoadFromConfig(configuration.GetSection("ReverseProxy"));
        return services;
    }

}
