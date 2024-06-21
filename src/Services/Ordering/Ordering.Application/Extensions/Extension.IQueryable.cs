namespace Ordering.Application.Extensions;
public partial class Extension
{
    public static async Task<PagedList<T>> ToPageListAsync<T>(this IQueryable<T> queryable, int pageNumber, int pageSize, CancellationToken token = default)
    {
        return await PagedList<T>.CreateAsync(queryable, pageNumber, pageSize, token)
                                 .ConfigureAwait(false);
    }
}
