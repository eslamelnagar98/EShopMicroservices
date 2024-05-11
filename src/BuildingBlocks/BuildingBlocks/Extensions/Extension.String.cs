namespace BuildingBlocks.Extensions;
public partial class Extension
{
    public static bool EqualTo(this string left, string right)
    {
        return left.Equals(right, StringComparison.OrdinalIgnoreCase);
    }
    public static bool IsNotNullOrEmpty(this string value) => !string.IsNullOrEmpty(value);
}
