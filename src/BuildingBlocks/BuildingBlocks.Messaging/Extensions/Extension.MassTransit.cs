namespace BuildingBlocks.Messaging.Extensions;
public static partial class Extension
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services, Assembly? assembly = null)
    {
        return services.AddMassTransit(RegisterBusConfigurator(assembly));
    }

    private static Action<IBusRegistrationConfigurator> RegisterBusConfigurator(Assembly? assembly)
    {
        return config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            if (assembly is not null)
            {
                config.AddConsumers(assembly);
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
