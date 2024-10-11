namespace Shopping.Web.Models.Ordering;
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

    private PagedList(int pageNumber, int pageSize, long count, IEnumerable<T> items)
    {
        _items.AddRange(items);
        TotalItemCount = count;
        PageNumber = pageNumber;
        PageSize = pageSize;
        PageCount = (long)Math.Ceiling(count / (double)pageSize);
    }

    public static PagedList<T> Create(int pageNumber, int pageSize, long count, IEnumerable<T> items)
        => new PagedList<T>(pageNumber, pageSize, count, items);

    public PagedList<TResult> Select<TResult>(Func<T, TResult> selector)
    {
        var projectedItems = _items.Select(selector)
                                   .ToList();

        return new PagedList<TResult>(PageNumber, PageSize, TotalItemCount, projectedItems);
    }


}