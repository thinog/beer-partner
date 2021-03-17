using BeerPartner.Domain.ValueObjects.GeoJSON;
using BeerPartner.Domain.Interfaces;
using BeerPartner.Domain.Entities.Base;
using System;
using System.Text.Json;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace BeerPartner.Domain.Entities
{
    public class Partner : BaseEntity<Guid>, IValidatable
    {
        // https://stackoverflow.com/questions/31526879/mongodb-c-sharp-driver-change-id-serialization-for-inherited-class
        [BsonId]
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string TradingName { get; set; }
        
        public string OwnerName { get; set; }
        
        public MultiPolygon CoverageArea { get; set; }

        public GeoJsonMultiPolygon<GeoJson2DCoordinates> CoverageAreaGeoJson => JsonSerializer.Deserialize<GeoJsonMultiPolygon<GeoJson2DCoordinates>>(JsonSerializer.Serialize(CoverageArea));
        
        public Point Address { get; set; }

        
        public GeoJsonPoint<GeoJson2DCoordinates> AddressGeoJson => JsonSerializer.Deserialize<GeoJsonPoint<GeoJson2DCoordinates>>(JsonSerializer.Serialize(Address));

        public bool Validate()
        {
            return 
                !string.IsNullOrWhiteSpace(TradingName)
                && !string.IsNullOrWhiteSpace(OwnerName)
                && CoverageArea.Validate()
                && Address.Validate();
        }
    }
}