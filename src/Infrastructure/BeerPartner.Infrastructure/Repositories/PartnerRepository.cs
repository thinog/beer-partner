using System;
using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using BeerPartner.Domain.Entities;
using BeerPartner.Application.Interfaces.Repositories;
using BeerPartner.Domain.Interfaces.Repositories.Context;
using BeerPartner.Domain.Utils;
using BeerPartner.Domain.ValueObjects.GeoJSON;

namespace BeerPartner.Infrastructure
{
    public sealed class PartnerRepository : Repository<Partner, Guid>, IPartnerRepository
    {
        public PartnerRepository(IDbContext<IAmazonDynamoDB> context) 
            : base(context) 
        {                
            Table = Table.LoadTable(context.Connection, Configuration.Get("DynamoDb.Partner.TableName"));
        }

        public Partner GetNearest(double longitude, double latitude)
        {
            throw new NotImplementedException();
        }

        public override Partner GetById(Guid id)
        {
            var result = Table.GetItemAsync(id.ToString()).Result;

            if(result == null || result.Count == 0)
                return null;

            var partner = new Partner
            {
                Id = result["Id"].AsGuid(),
                TradingName = result["TradingName"].AsString(),
                OwnerName = result["OwnerName"].AsString(),
                Address = JsonSerializer.Deserialize<Point>(result["Address"].AsString()),
                CoverageArea = JsonSerializer.Deserialize<MultiPolygon>(result["CoverageArea"].AsString())
            };

            return partner;
        }

        public override Guid Insert(Partner entity)
        {
            var document = new Document();

            var id = Guid.NewGuid();

            document["Id"] = id;
            document["TradingName"] = entity.TradingName;
            document["OwnerName"] = entity.OwnerName;
            document["Address"] = JsonSerializer.Serialize(entity.Address);
            document["CoverageArea"] = JsonSerializer.Serialize(entity.CoverageArea);

            Table.PutItemAsync(document).GetAwaiter().GetResult();

            return id;
        }
    }
}