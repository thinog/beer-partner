using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using BeerPartner.Domain.Interfaces.Repositories;
using BeerPartner.Domain.Interfaces.Repositories.Context;

namespace BeerPartner.Infrastructure
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    {
        protected readonly IDbContext<IAmazonDynamoDB> Context;
        protected Table Table;

        public Repository(IDbContext<IAmazonDynamoDB> context)
        {
            Context = context;
        }

        public virtual TEntity GetById(TKey id)
        {
            throw new NotImplementedException();
        }

        public virtual TKey Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}