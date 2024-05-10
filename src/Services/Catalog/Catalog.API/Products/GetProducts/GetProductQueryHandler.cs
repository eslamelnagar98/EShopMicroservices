namespace Catalog.API.Products.GetProducts;
internal sealed class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
                                    .ToCatalogPagedListAsync(query.PageNumber, query.PageSize, cancellationToken);
        return new GetProductsResult(products);
    }
}
