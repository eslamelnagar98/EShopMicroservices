namespace BuildingBlocks.Messaging.Extensions;
public partial class Extension
{
    public static bool IsNullOrEmpty<T>([NotNullWhen(false)] this IEnumerable<T> collection)
    {
        return collection is null || !collection.Any();
    }

    public static bool IsNotNullOrEmpty<T>([NotNullWhen(true)] this IEnumerable<T> collection)
    {
        return !collection.IsNullOrEmpty();
    }

}
