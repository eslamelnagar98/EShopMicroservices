namespace Catalog.API.Products.DeleteProduct;
internal sealed record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
internal sealed record DeleteProductResult(bool IsSuccess);
