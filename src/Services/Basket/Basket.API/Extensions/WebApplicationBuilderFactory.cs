namespace Basket.API.Extensions;
public static class WebApplicationBuilderFactory
{
    public static WebApplicationBuilder CreateBasketWebBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseNLog();
        builder
            .Services
            //.TryAddInitializeMartenWith<CatalogInitialData>()
            .AddIOptions<DatabaseSettingsOptions>(DatabaseSettingsOptions.SectionName)
            .AddBasketServices()
            ;
        return builder;
    }
}
