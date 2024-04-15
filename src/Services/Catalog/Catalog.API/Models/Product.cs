namespace Catalog.API.Models;
public sealed class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public List<string> Category { get; private set; } = new();
    public string Description { get; private set; } = default!;
    public string ImageFile { get; private set; } = default!;
    public decimal Price { get; private set; }

    public static Product Initialize() => new();
    public Product Create(CreateProductCommand command)
    {
        Name = command.Name;
        Category = command.Category;
        Description = command.Description;
        ImageFile = command.ImageFile;
        Price = command.Price;
        return this;
    }
}
