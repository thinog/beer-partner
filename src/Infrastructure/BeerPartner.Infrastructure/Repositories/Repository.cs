using System;
using BeerPartner.Domain.Entities.Base;
using BeerPartner.Domain.Interfaces.Repositories;
using BeerPartner.Domain.Interfaces.Repositories.Context;
using MongoDB.Driver;

namespace BeerPartner.Infrastructure.Repositories
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey> 
        where TEntity : BaseEntity<TKey>
    {
        protected readonly IDbContext<IMongoClient> Context;
        protected readonly IMongoDatabase Database;
        protected IMongoCollection<TEntity> Collection;

        public Repository(IDbContext<IMongoClient> context)
        {
            Context = context;
            Database = context.Connection.GetDatabase(context.DatabaseName);
        }

        public virtual TEntity GetById(TKey id)
        {
			return Collection?.Find(Builders<TEntity>.Filter.Eq("_id", id))?.SingleOrDefault();
        }

        public virtual TKey Insert(TEntity entity)
        {   
            Collection?.InsertOne(entity);
            
            return default(TKey);
        }
    }
}