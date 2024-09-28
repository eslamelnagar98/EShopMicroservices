namespace Basket.API.Extensions;
public partial class Extension
{
    public static IServiceCollection AddCarterWithAssemblies(this IServiceCollection services, params Assembly[] assemblies)
    {
       return services.AddCarter(configurator: carterConfigurator =>
        {
            var modules = assemblies.SelectMany(assembly =>
                                     assembly.GetTypes()
                                             .Where(type => !type.IsAbstract &&
                                                    typeof(ICarterModule).IsAssignableFrom(type) &&
                                                    type != typeof(ICarterModule) &&
                                                    type.IsPublic))
                                    .ToArray();

            carterConfigurator.WithModules(modules);
        });
    }
}

