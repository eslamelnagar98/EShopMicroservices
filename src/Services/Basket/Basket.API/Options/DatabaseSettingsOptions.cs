namespace Basket.API.Options.Validations;
public class DatabaseSettingsOptions
{
    public const string SectionName = "DatabaseSettings";
    public string ConnectionString { get; set; }
}
