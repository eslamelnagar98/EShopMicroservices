namespace Catalog.API.Products.CreateProduct;
public class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = Product
                        .Initialize()
                        .Create(command);
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken); 
        return new CreateProductResult(product.Id);
    }
}
