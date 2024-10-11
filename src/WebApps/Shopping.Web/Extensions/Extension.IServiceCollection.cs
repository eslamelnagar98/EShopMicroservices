namespace Shopping.Web.Extensions;
public partial class Extension
{
    public static IServiceCollection AddCustomFluentValidation(this IServiceCollection services, Assembly assembly)
    {
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true)
                .AddFluentValidationClientsideAdapters();
        return services;
    }

    public static IServiceCollection AddApiSettingsOptions(this IServiceCollection services, string sectionName)
    {
        services.AddIOptions<ApiSettingsOptions>(ApiSettingsOptions.SectionName);
        return services;
    }

    public static IServiceCollection AddCustomRefitClients(this IServiceCollection services)
    {
        services.AddRefitClients();
        return services;
    }

    public static IServiceCollection AddCustomRazorPages(this IServiceCollection services)
    {
        services.AddRazorPages();
        return services;
    }

}
