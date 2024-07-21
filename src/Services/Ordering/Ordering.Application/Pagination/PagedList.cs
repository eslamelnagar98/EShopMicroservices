namespace Ordering.Application.Pagination;
public class PagedList<T>
{
    private readonly List<T> _items = new List<T>();
    public IReadOnlyCollection<T> Items => _items.AsReadOnly();
    public T this[int index] => _items[index];
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }
    public long PageCount { get; private set; }
    public long TotalItemCount { get; private set; }
    public bool HasPreviousPage => PageNumber > 0;
    public bool HasNextPage => PageNumber < PageCount - 1;
    public bool IsFirstPage => PageNumber == 0;
    public bool IsLastPage => PageNumber >= PageCount - 1;
    public long FirstItemOnPage => Math.Min(TotalItemCount, PageNumber * PageSize + 1);
    public long LastItemOnPage => Math.Min(FirstItemOnPage + PageSize - 1, TotalItemCount);

    private PagedList(IEnumerable<T> items, long count, int pageNumber, int pageSize)
    {
        _items.AddRange(items);
        TotalItemCount = count;
        PageNumber = pageNumber;
        PageSize = pageSize;
        PageCount = (long)Math.Ceiling(count / (double)pageSize);
    }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> queryable, int pageNumber, int pageSize, CancellationToken token = default)
    {
        PageNumberLessThanZeroException.ThrowIfLessThanZero(pageNumber);

        PageSizeLessThanOneException.ThrowIfLessThanOne(pageSize);

        var totalCount = await queryable.CountAsync(token)
                                        .ConfigureAwait(false);

        var items = await GetQueryableDataAsListAsync(queryable, pageNumber, pageSize, token);

        return new PagedList<T>(items, totalCount, pageNumber, pageSize);
    }

    public PagedList<TResult> Select<TResult>(Func<T, TResult> selector)
    {
        var projectedItems = _items.Select(selector)
                                   .ToList();

        return new PagedList<TResult>(projectedItems, TotalItemCount, PageNumber, PageSize);
    }

    private static async Task<List<T>> GetQueryableDataAsListAsync(IQueryable<T> queryable, int pageNumber, int pageSize, CancellationToken token = default)
    {
        if (pageNumber < 0)
        {
            pageNumber = 0;
        }

        return await queryable.Skip(pageNumber * pageSize)
                              .Take(pageSize)
                              .ToListAsync(token)
                              .ConfigureAwait(false);
    }
}


