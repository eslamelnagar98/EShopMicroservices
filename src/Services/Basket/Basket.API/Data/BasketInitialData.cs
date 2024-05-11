namespace Basket.API.Data;
public class BasketInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        var isAnyProduct = await session.Query<ShoppingCart>()
                                        .AnyAsync();
        if (isAnyProduct)
        {
            return;
        }
        session.Store(GetPreconfiguredShoppingCart());
        await session.SaveChangesAsync();
    }

    private static ShoppingCart GetPreconfiguredShoppingCart()
    {
        return new()
        {
            UserName = "swn",
            Items = new List<ShoppingCartItem>
            {
                   new ()
                   {
                       Quantity = 2,
                       Color = "Red",
                       Price = 500,
                       ProductId = Guid.Parse("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
                       ProductName = "IPhone X"
                   },
                   new ()
                   {
                       Quantity = 1,
                       Color = "Blue",
                       Price = 500,
                       ProductId = Guid.Parse("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),
                       ProductName = "Samsung 10"
                   }
            }
        };
    }
}
