namespace BuildingBlocks.Extensions;
public static partial class Extension
{
    public static T GetSafeValue<T>(this T? value, T defaultValue = default)
      where T : struct, INumber<T>
    {
        return value.GetValueOrDefault(defaultValue);
    }
}