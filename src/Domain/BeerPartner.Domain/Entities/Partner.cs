using BeerPartner.Domain.ValueObjects.GeoJSON;
using BeerPartner.Domain.Interfaces;
using System.Text.Json.Serialization;
using System;

namespace BeerPartner.Domain.Entities
{
    public class Partner : IValidatable
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        
        [JsonPropertyName("tradingName")]
        public string TradingName { get; set; }
        
        [JsonPropertyName("ownerName")]
        public string OwnerName { get; set; }
        
        [JsonPropertyName("coverageArea")]
        public MultiPolygon CoverageArea { get; set; }
        
        [JsonPropertyName("address")]
        public Point Address { get; set; }

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