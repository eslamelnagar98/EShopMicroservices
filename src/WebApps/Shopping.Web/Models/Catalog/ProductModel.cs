namespace Shopping.Web.Models.Catalog;
public class ProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Category { get; set; } = new();
    public string Description { get; set; } = string.Empty;
    public string ImageFile { get; set; } = string.Empty;
    public decimal Price { get; set; } = decimal.Zero;
}
public class CatalogPageList<T>
{
    public long TotalItemCount { get;  set; }
    public long PageNumber { get;  set; }
    public long PageSize { get;  set; }
    public long PageCount { get;  set; }
    public bool HasPreviousPage { get;  set; }
    public bool HasNextPage { get;  set; }
    public IEnumerable<T> Items { get;  set; } = [];
}
public sealed record GetProductsResponse(CatalogPageList<ProductModel> Products);
public sealed record GetProductByCategoryResponse(IEnumerable<ProductModel> Products);
public sealed record GetProductByIdResponse(ProductModel Product);