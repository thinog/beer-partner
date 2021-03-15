namespace BeerPartner.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity, TKey>
    {
        TEntity GetById(TKey id);
        TKey Insert(TEntity entity);
    }
}