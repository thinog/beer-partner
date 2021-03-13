using System.Collections.Generic;
using System.Text.Json.Serialization;
using BeerPartner.Domain.Enums;
using BeerPartner.Domain.Interfaces.ValueObjects;

namespace BeerPartner.Domain.ValueObjects.GeoJSON
{
    // https://tools.ietf.org/html/rfc7946#section-3.1.4
    public class LineString : IGeometry<Point>
    {
        [JsonPropertyName("type")]
        public GeometryType Type => GeometryType.LineString;
        
        [JsonPropertyName("coordinates")]
        public IEnumerable<Point> Coordinates { get; set; }
    }
}