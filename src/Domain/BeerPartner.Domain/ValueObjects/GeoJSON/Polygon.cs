using System.Collections.Generic;
using System.Text.Json.Serialization;
using BeerPartner.Domain.Enums;
using BeerPartner.Domain.Interfaces.ValueObjects;

namespace BeerPartner.Domain.ValueObjects.GeoJSON
{
    // https://tools.ietf.org/html/rfc7946#section-3.1.6
    public class Polygon : IGeometry<LineString>
    {
        [JsonPropertyName("type")]
        public GeometryType Type => GeometryType.Polygon;
        
        [JsonPropertyName("coordinates")]
        public IEnumerable<LineString> Coordinates { get; set; }
    }
}