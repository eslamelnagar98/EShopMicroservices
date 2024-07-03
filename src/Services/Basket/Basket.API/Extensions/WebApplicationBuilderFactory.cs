namespace Basket.API.Extensions;
public static class WebApplicationBuilderFactory
{
    public static WebApplicationBuilder CreateBasketWebBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseNLog();
        builder
            .TryAddInitializeMartenWith<BasketInitialData>()
            .AddIOptions<PersistenceSettingsOptions>(PersistenceSettingsOptions.SectionName)
            .AddIOptions<GrpcSettingsOptions>(GrpcSettingsOptions.SectionName)
            .AddIOptions<MessageBrokerOptions>(MessageBrokerOptions.SectionName)
            .AddBasketServices()
            ;
        return builder;
    }
}
