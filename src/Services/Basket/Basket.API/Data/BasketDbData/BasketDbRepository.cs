
namespace Basket.API.Data.BasketDbData;
internal sealed class BasketDbRepository(IDocumentSession session) : IBasketDbRepository
{
    public async Task<IReadOnlyList<TEntity>> GetAllDataAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await session.Query<TEntity>()
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public async Task<TEntity> StoreEntity<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
    {
        session.Store(entity);

        await session.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public void StoreOutboxMessage(OutboxMessage outboxMessage)
    {
        session.Store(outboxMessage);
    }
}
