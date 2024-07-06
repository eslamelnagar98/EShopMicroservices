namespace Basket.API.Data.BasketDbData;
internal interface IBasketDbRepository
{
    Task<IReadOnlyList<TEntity>> GetAllDataAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    Task<TEntity> StoreEntity<TEntity>(TEntity entity, CancellationToken cancellationToken = default);
    void StoreOutboxMessage(OutboxMessage outboxMessage);
}
