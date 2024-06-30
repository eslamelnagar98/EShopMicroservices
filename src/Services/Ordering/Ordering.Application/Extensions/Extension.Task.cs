namespace Ordering.Application.Extensions;
public partial class Extension
{ 
    public static async Task WhenAllWithAggregatedExceptions(this IEnumerable<Task> tasks)
    {
        var allTasks = tasks.ToArray();

        try
        {
            await Task.WhenAll(allTasks);
        }
        catch
        {
            var exceptions = allTasks
                .Where(t => t.IsFaulted)
                .SelectMany(t => t.Exception?.InnerExceptions ?? Enumerable.Empty<Exception>())
                .ToList();

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
            throw;
        }
    }

}
