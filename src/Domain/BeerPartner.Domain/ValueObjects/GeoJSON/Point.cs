using System.Collections.Generic;
using System.Text.Json.Serialization;
using BeerPartner.Domain.Enums;
using BeerPartner.Domain.Interfaces.ValueObjects;

namespace BeerPartner.Domain.ValueObjects.GeoJSON
{
    // https://tools.ietf.org/html/rfc7946#section-3.1.2
    public class Point : IGeometry<decimal>
    {
        [JsonPropertyName("type")]
        public GeometryType Type => GeometryType.Polygon;
        
        [JsonPropertyName("coordinates")]
        public IEnumerable<decimal> Coordinates { get; set; }
    }
}