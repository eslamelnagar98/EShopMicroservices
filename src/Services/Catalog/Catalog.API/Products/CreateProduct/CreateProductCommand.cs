namespace Catalog.API.Products.CreateProduct;
public record GetProductQuery(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);
