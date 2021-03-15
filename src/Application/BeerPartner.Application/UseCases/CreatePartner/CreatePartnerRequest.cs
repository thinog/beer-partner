using System;
using System.Text.Json.Serialization;
using BeerPartner.Domain.Entities;
using BeerPartner.Domain.ValueObjects.GeoJSON;

namespace BeerPartner.Application.UseCases.CreatePartner
{
    public class CreatePartnerRequest
    {        
        [JsonPropertyName("tradingName")]
        public string TradingName { get; set; }
        
        [JsonPropertyName("ownerName")]
        public string OwnerName { get; set; }
        
        [JsonPropertyName("coverageArea")]
        public MultiPolygon CoverageArea { get; set; }
        
        [JsonPropertyName("address")]
        public Point Address { get; set; }

        internal Partner ToPartner()
        {
            return new Partner
            {
                TradingName = TradingName,
                OwnerName = OwnerName,
                CoverageArea = CoverageArea,
                Address = Address
            };
        }
    }
}