namespace Catalog.API.Products.UpdateProduct;
internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }
        var updatedProduct = product.Update(command);
        session.Update(updatedProduct);
        await session.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
    }
}
