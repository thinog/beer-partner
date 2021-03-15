using System;
using System.Text.Json.Serialization;
using BeerPartner.Domain.Entities;
using BeerPartner.Domain.ValueObjects.GeoJSON;

namespace BeerPartner.Application.UseCases.GetPartner
{
    public class GetPartnerResponse
    {
        public GetPartnerResponse(Partner partner)
        {
            Id = partner.Id;
            TradingName = partner.TradingName;
            OwnerName = partner.OwnerName;
            CoverageArea = partner.CoverageArea;
            Address = partner.Address;
        }

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
    }
}