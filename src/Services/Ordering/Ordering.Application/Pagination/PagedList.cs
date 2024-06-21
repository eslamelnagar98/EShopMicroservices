namespace Ordering.Application.Pagination;
public class PagedList<T> : IEnumerable<T>, IEnumerable
{
    private readonly List<T> _items = new();
    public T this[int index] => _items[index];
    public long Count => _items.Count;
    public int PageNumber { get; private set; }
    public int PageSize { get; private set; }
    public long PageCount { get; private set; }
    public long TotalItemCount { get; private set; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < PageCount;
    public bool IsFirstPage => PageNumber == 1;
    public bool IsLastPage => PageNumber >= PageCount;
    public long FirstItemOnPage => (PageNumber - 1) * PageSize + 1;
    public long LastItemOnPage => Math.Min(FirstItemOnPage + PageSize - 1, TotalItemCount);

    private PagedList(IEnumerable<T> items, long count, int pageNumber, int pageSize)
    {
        _items.AddRange(items);
        TotalItemCount = count;
        PageNumber = pageNumber;
        PageSize = pageSize;
        PageCount = (long)Math.Ceiling(count / (double)pageSize);
    }

    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> queryable, int pageNumber, int pageSize, CancellationToken token = default)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber), "PageNumber cannot be below 1.");
        }

        if (pageSize < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize), "PageSize cannot be below 1.");
        }

        var totalCount = await queryable.CountAsync(token)
                                        .ConfigureAwait(false);

        var items = await queryable.Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync(token)
                                   .ConfigureAwait(false);

        return new PagedList<T>(items, totalCount, pageNumber, pageSize);
    }

    public PagedList<TResult> Select<TResult>(Func<T, TResult> selector)
    {
        var projectedItems = _items.Select(selector)
                                   .ToList();

        return new PagedList<TResult>(projectedItems, TotalItemCount, PageNumber, PageSize);
    }
}


