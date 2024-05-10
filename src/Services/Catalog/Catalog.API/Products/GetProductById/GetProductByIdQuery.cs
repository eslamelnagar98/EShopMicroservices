namespace Catalog.API.Products.GetProductById;
internal sealed record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
internal sealed record GetProductByIdResult(Product Product);
