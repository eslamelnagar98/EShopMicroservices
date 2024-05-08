namespace Catalog.API.Models;
public class CatalogPageList<T>
{
    public long TotalItemCount { get; private set; }
    public long PageNumber { get; private set; }
    public long PageSize { get; private set; }
    public long PageCount { get; private set; }
    public bool HasPreviousPage { get; private set; }
    public bool HasNextPage { get; private set; }
    public IEnumerable<T> Items { get; private set; }

    public static CatalogPageList<T> Create(IPagedList<T> pageList)
    {
        return new()
        {
            TotalItemCount = pageList.TotalItemCount,
            PageNumber = pageList.PageNumber,
            PageSize = pageList.PageSize,
            PageCount = pageList.PageCount,
            HasNextPage = pageList.HasNextPage,
            HasPreviousPage = pageList.HasPreviousPage,
            Items = pageList
        };
    }
}
