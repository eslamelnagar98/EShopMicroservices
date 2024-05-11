namespace Discount.gRPC.Options;
internal sealed class DatabaseSettingsOptions
{
    public const string SectionName = "DatabaseSettings";
    public string ConnectionString { get; set; }
}
