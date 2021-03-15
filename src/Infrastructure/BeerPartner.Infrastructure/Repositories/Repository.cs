using BeerPartner.Domain.Interfaces.Repositories;

namespace BeerPartner.Infrastructure
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    {
        public virtual TEntity GetById(TKey id)
        {
            throw new System.NotImplementedException();
        }

        public virtual TKey Insert(TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}