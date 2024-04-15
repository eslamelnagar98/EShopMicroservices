namespace Catalog.API.Configurations;
public static class WebApplicationBuilderFactory
{
    public static WebApplicationBuilder CreateCatalogWepBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseNLog();
        builder
            .Services
            .AddCatalogOptions()
            .AddCatalogServices();
        return builder;
    }
}
