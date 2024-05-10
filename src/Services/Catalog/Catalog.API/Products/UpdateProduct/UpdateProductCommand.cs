namespace Catalog.API.Products.UpdateProduct;
internal sealed record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateProductResult>;
internal sealed record UpdateProductResult(bool IsSuccess);
