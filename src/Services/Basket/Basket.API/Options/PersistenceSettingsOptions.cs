namespace Basket.API.Options.Validations;
public class PersistenceSettingsOptions
{
    public const string SectionName = "DatabaseSettings";
    public string ConnectionString { get; set; }
    public string Redis { get; set; }
    public string Cron { get; set; }
}
