namespace Shopping.Web.Extensions;
public partial class Extension
{
    public static IHostBuilder UseCustomNLog(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseNLog();
        return hostBuilder;
    }
}
