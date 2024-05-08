namespace Catalog.API.Products.GetProducts;
public record GetProductsQuery(int PageNumber = 1, int PageSize = 10) : IQuery<GetProductsResult>;
public record GetProductsResult(CatalogPageList<Product> Products);
