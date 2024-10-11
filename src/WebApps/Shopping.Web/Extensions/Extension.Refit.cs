namespace Shopping.Web.Extensions;
public partial class Extension
{
    public static IServiceCollection AddRefitClients(this IServiceCollection services)
    {
        services.AddLeasingIhcRefitClient<IBasketService>(
                   serviceProvider => Guard.Against.Null(serviceProvider.GetRequiredService<IOptions<ApiSettingsOptions>>().Value.GatewayAddress))
                .AddLeasingIhcRefitClient<ICatalogService>(
                    serviceProvider => Guard.Against.Null(serviceProvider.GetRequiredService<IOptions<ApiSettingsOptions>>().Value.GatewayAddress))
                .AddLeasingIhcRefitClient<IOrderingService>(
                    serviceProvider => Guard.Against.Null(serviceProvider.GetRequiredService<IOptions<ApiSettingsOptions>>().Value.GatewayAddress));

        return services;
    }

    /// <summary>
    /// Adds a Refit client to the services collection.
    /// </summary>
    /// <typeparam name="TRefitClient">The type of the Refit client.</typeparam>
    /// <param name="services">The IServiceCollection to add the client to.</param>
    /// <param name="baseAddressUrlFunc">A function to determine the base address for the HTTP client.</param>
    /// <returns>The updated IServiceCollection.</returns>
    public static IServiceCollection AddLeasingIhcRefitClient<TRefitClient>(this IServiceCollection services, Func<IServiceProvider, string> baseAddressUrlFunc)
        where TRefitClient : class
    {
        var refitSetting = GenerateRefitSettings();

        services.AddLeasingIhcRefitClientInternal<TRefitClient>(refitSetting, baseAddressUrlFunc);

        return services;
    }


    private static IHttpClientBuilder AddLeasingIhcRefitClientInternal<TRefitClient>(this IServiceCollection services,
                                                                                     RefitSettings refitSettings,
                                                                                     Func<IServiceProvider, string> baseAddressUrlFunc)
        where TRefitClient : class
    {
        return services.AddRefitClient<TRefitClient>(refitSettings).ConfigureHttpClient((serviceProvider, client) =>
        {
            var baseAddressUrl = baseAddressUrlFunc(serviceProvider);

            client.BaseAddress = new Uri(baseAddressUrl);
        }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        }).LogRequestResponse(p =>
        {
            p.ResponseBodyLogMode = LogMode.LogAll;
            p.RequestBodyLogTextLengthLimit = 1000000;
            p.ResponseBodyLogTextLengthLimit = 1000000;
            p.MaskedProperties.Clear();
        });

    }

    private static RefitSettings GenerateRefitSettings()
    {
        return new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            })
        };
    }
}
