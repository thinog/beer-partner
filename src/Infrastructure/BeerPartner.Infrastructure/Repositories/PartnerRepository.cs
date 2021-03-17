using System;
using BeerPartner.Domain.Entities;
using BeerPartner.Application.Interfaces.Repositories;
using BeerPartner.Domain.Interfaces.Repositories.Context;
using BeerPartner.Domain.Utils;
using Amazon.DynamoDBv2.DataModel;

namespace BeerPartner.Infrastructure.Repositories
{
    public sealed class PartnerRepository : Repository<Partner, Guid>, IPartnerRepository
    {
        public PartnerRepository(IDbContext<IDynamoDBContext> context) 
            : base(context) 
        {   
            DynamoConfig = new DynamoDBOperationConfig 
            {
                OverrideTableName = Configuration.Get("DynamoDb.Partner.TableName")
            };
        }

        public Partner GetNearest(double longitude, double latitude)
        {
            throw new NotImplementedException();
        }

        public override Guid Insert(Partner entity)
        {
            entity.Id = Guid.NewGuid();
            return base.Insert(entity);
        }
    }
}