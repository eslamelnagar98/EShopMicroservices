namespace BuildingBlocks.Extensions;
public partial class Extension
{
    public static async Task ForEachAsync<T>(this IEnumerable<T> colletion, Func<T, Task> func)
    {
        foreach (var item in colletion)
        {
            await func(item);
        }
    }

}
