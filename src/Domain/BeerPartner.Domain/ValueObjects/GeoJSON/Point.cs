using System.Text.Json.Serialization;
using BeerPartner.Domain.Converters.GeoJSON;
using BeerPartner.Domain.Enums;
using BeerPartner.Domain.Interfaces.ValueObjects;

namespace BeerPartner.Domain.ValueObjects.GeoJSON
{
    [JsonConverter(typeof(GeometryConverter<Point, Position>))]
    // https://tools.ietf.org/html/rfc7946#section-3.1.2
    public class Point : IGeometry<Position>
    {
        public Point() { }

        [JsonPropertyName("type")]
        public GeometryType Type => GeometryType.Point;
        
        [JsonPropertyName("coordinates")]
        public Position Coordinates { get; set; }
        
        [JsonIgnore]
        public bool IsRoot { get; set; }

        public bool Validate()
        {
            return 
                Coordinates != null
                && Coordinates.Validate();
        }
    }
}