namespace Catalog.API.Models;
public sealed class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Category { get; set; } = new();
    public string Description { get; set; } = string.Empty;
    public string ImageFile { get; set; } = string.Empty;
    public decimal Price { get; set; } = decimal.Zero;
    public static Product Initialize() => new();
    internal Product Create(GetProductQuery query)
    {
        Name = query.Name;
        Category = query.Category;
        Description = query.Description;
        ImageFile = query.ImageFile;
        Price = query.Price;
        return this;
    }
    internal Product Update(UpdateProductCommand command)
    {
        Name = command.Name;
        Category = command.Category;
        Description = command.Description;
        ImageFile = command.ImageFile;
        Price = command.Price;
        return this;
    }
}
