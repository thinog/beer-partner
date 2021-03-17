using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using BeerPartner.Domain.Entities.Base;
using BeerPartner.Domain.Interfaces.Repositories;
using BeerPartner.Domain.Interfaces.Repositories.Context;

namespace BeerPartner.Infrastructure.Repositories
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey> 
        where TEntity : BaseEntity<TKey>
    {
        protected readonly IDbContext<IDynamoDBContext> Context;
        protected DynamoDBOperationConfig DynamoConfig;

        public Repository(IDbContext<IDynamoDBContext> context)
        {
            Context = context;
        }

        public virtual TEntity GetById(TKey id)
        {
            return Context.Connection.LoadAsync<TEntity>(id, DynamoConfig).Result;
        }

        public virtual TKey Insert(TEntity entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            
            Context.Connection.SaveAsync<TEntity>(entity, DynamoConfig).GetAwaiter().GetResult();
            
            return entity.Id;
        }
    }
}