namespace Catalog.API.Products.GetProducts;
internal sealed record GetProductsQuery(int PageNumber = 1, int PageSize = 10) : IQuery<GetProductsResult>;
internal sealed record GetProductsResult(CatalogPageList<Product> Products);
