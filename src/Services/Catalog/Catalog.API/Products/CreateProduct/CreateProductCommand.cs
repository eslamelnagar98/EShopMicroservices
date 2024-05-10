namespace Catalog.API.Products.CreateProduct;
internal sealed record GetProductQuery(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
internal sealed record CreateProductResult(Guid Id);
