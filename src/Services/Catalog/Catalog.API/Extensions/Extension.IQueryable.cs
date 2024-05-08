namespace Catalog.API.Extensions;
public partial class Extension
{
    public static async Task<CatalogPageList<T>> ToCatalogPagedListAsync<T>(this IQueryable<T> queryable, int pageNumber, int pageSize, CancellationToken token = default)
    {
        var pageList = await PagedList<T>.CreateAsync(queryable, pageNumber, pageSize, token)
                                         .ConfigureAwait(false);

        return CatalogPageList<T>.Create(pageList);
    }
}
