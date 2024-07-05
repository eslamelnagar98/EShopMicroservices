namespace Basket.API.Data.BasketDbData;
internal interface IBasketDbRepository
{
    Task<TEntity> StoreEntity<TEntity>(TEntity entity, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAllDataAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
}
