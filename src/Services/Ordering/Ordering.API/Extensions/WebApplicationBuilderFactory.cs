namespace Ordering.API.Extensions;
public static class WebApplicationBuilderFactory
{
    public static WebApplicationBuilder CreateOrderingWebBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseNLog();
        builder
            .Services
            .AddIOptions<DatabaseOptions>(DatabaseOptions.SectionName)
            .AddIOptions<MessageBrokerOptions>(MessageBrokerOptions.SectionName)
            .AddOrderingServices()
            .AddOrderingDbContext<ApplicationDbContext>()
            ;
        return builder;
    }
}