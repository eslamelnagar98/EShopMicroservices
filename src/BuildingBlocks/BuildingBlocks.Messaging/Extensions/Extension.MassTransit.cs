namespace BuildingBlocks.Messaging.Extensions;
public static partial class Extension
{
    public static IServiceCollection AddMessageBrokerWithConsumers(this IServiceCollection services, params Assembly[] assemblies)
    {
        return services.AddMessageBroker(assemblies);
    }

    public static IServiceCollection AddMessageBroker(this IServiceCollection services)
    {
        return services.AddMessageBroker();
    }

    private static IServiceCollection AddMessageBroker(this IServiceCollection services, params Assembly[] assemblies)
    {
        return services.AddMassTransit(RegisterBusConfigurator(assemblies));
    }

    private static Action<IBusRegistrationConfigurator> RegisterBusConfigurator(params Assembly[] assemblies)
    {
        return config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            if (assemblies.IsNotNullOrEmpty())
            {
                config.AddConsumers(assemblies);
            }

            config.UsingRabbitMq(ConfigureRabbitMQ);
        };
    }

    private static void ConfigureRabbitMQ(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator configurator)
    {
        var messageBrokerOptions = Guard.Against.Null(context.GetRequiredService<IOptions<MessageBrokerOptions>>().Value);

        configurator.Host(new Uri(messageBrokerOptions.Host), host =>
        {
            host.Username(messageBrokerOptions.UserName);
            host.Password(messageBrokerOptions.Password);
        });

        configurator.ConfigureEndpoints(context);
    }


}
