namespace Discount.gRPC.Extensions;
public class WebApplicationBuilderFactory
{
    public static WebApplicationBuilder CreateDiscountWebBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseNLog();
        builder
            .Services
            .AddIOptions<DatabaseSettingsOptions>(DatabaseSettingsOptions.SectionName)
            .AddDiscountServices()
            ;
        return builder;
    }
}
