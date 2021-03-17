using System;
using BeerPartner.Domain.Entities;
using BeerPartner.Application.Interfaces.Repositories;
using BeerPartner.Domain.Interfaces.Repositories.Context;
using BeerPartner.Domain.Utils;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using BeerPartner.Domain.ValueObjects.GeoJSON;
using System.Text.Json;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;
using System.Linq;

namespace BeerPartner.Infrastructure.Repositories
{
    public sealed class PartnerRepository : Repository<Partner, Guid>, IPartnerRepository
    {
        public PartnerRepository(IDbContext<IMongoClient> context) 
            : base(context) 
        {   
            Collection = Database.GetCollection<Partner>(Configuration.Get("MongoDB.CollectionName.Partner"));

            // BsonClassMap.RegisterClassMap<Partner>(cm =>
            // {
                // cm.AutoMap();

                // cm.SetIsRootClass(true);
                // cm.MapIdProperty(p => p.Id).SetIdGenerator(GuidGenerator.Instance);
                
                // cm.MapMember(p => p.OwnerName);
                // cm.MapMember(p => p.TradingName);
                // cm.MapMember(p => p.CreatedAt);

                // cm.UnmapMember(p => p.CoverageArea);
                // cm.MapMember(p => p.CoverageAreaGeoJson).ToJson();
                
                // cm.UnmapMember(p => p.Address);
                // cm.MapMember(p => p.AddressGeoJson).ToJson();
            // });
        }

        public Partner GetNearest(double longitude, double latitude)
        {
            var location = new GeoJson2DCoordinates(longitude, latitude);
            var geometry = new GeoJsonPoint<GeoJson2DCoordinates>(location);

            var intersectFilter = new FilterDefinitionBuilder<Partner>()
                .GeoIntersects(
                    p => p.CoverageAreaGeoJson,
                    geometry);

            var nearFilter =  new FilterDefinitionBuilder<Partner>()
                .Near(
                    p => p.AddressGeoJson,
                    geometry);

            var partners = Collection?.Find(intersectFilter & nearFilter)?.ToList();

            if(partners == null || !partners.Any())
                return null;

            return partners.First();
        }

        public override Partner GetById(Guid id)
        {
            return base.GetById(id);
        }

        public override Guid Insert(Partner entity)
        {
            entity.CreatedAt = DateTime.UtcNow;

            Collection?.InsertOne(entity);

            return entity.Id;
        }
    }
}