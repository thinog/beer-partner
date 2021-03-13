using System.Collections.Generic;
using System.Text.Json.Serialization;
using BeerPartner.Domain.Enums;
using BeerPartner.Domain.Interfaces.ValueObjects;

namespace BeerPartner.Domain.ValueObjects.GeoJSON
{
    // https://tools.ietf.org/html/rfc7946#section-3.1.7
    public class MultiPolygon : IGeometry<Polygon>
    {
        [JsonPropertyName("type")]
        public GeometryType Type => GeometryType.MultiPolygon;
        
        [JsonPropertyName("coordinates")]
        public IEnumerable<Polygon> Coordinates { get; set; }
    }
}