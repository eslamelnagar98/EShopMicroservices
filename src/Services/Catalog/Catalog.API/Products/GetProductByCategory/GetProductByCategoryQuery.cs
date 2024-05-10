namespace Catalog.API.Products.GetProductByCategory;
internal sealed record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
internal sealed record GetProductByCategoryResult(IEnumerable<Product> Products);