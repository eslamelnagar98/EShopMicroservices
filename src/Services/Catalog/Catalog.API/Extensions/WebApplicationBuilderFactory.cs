namespace Catalog.API.Extensions;
public static class WebApplicationBuilderFactory
{
    public static WebApplicationBuilder CreateCatalogWebBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseNLog();
        builder
            .TryAddInitializeMartenWith<CatalogInitialData>()
            .AddCatalogOptions<DatabaseSettingsOptions>(DatabaseSettingsOptions.SectionName)
            .AddCatalogServices()
            ;
        return builder;
    }
}
