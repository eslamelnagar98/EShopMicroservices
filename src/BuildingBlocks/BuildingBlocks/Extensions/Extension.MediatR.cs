namespace BuildingBlocks.Extensions;
public partial class Extension
{
    public static IServiceCollection AddMediatorBehaviors(this IServiceCollection services, Assembly assembly)
    {
        return services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
    }

    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
    {
        return services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
        });
    }

}
