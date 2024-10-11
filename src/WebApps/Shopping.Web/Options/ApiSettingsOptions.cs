namespace Shopping.Web.Options;
public class ApiSettingsOptions
{
    public const string SectionName = "ApiSettings";
    public string GatewayAddress { get; set; } = string.Empty;
}
