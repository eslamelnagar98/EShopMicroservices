namespace Catalog.API.Products.CreateProduct;
public class GetProductQueryHandler(IDocumentSession session) : ICommandHandler<GetProductQuery, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(GetProductQuery command, CancellationToken cancellationToken)
    {
        var product = Product
                        .Initialize()
                        .Create(command);
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken); 
        return new CreateProductResult(product.Id);
    }
}
