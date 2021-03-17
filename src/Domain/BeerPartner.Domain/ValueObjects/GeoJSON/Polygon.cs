using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using BeerPartner.Domain.Converters.GeoJSON;
using BeerPartner.Domain.Enums;
using BeerPartner.Domain.Interfaces.ValueObjects;

namespace BeerPartner.Domain.ValueObjects.GeoJSON
{
    [JsonConverter(typeof(GeometryConverter<Polygon, List<LineString>>))]
    // https://tools.ietf.org/html/rfc7946#section-3.1.6
    public class Polygon : IGeometry<List<LineString>>
    {
        public Polygon() { }

        [JsonPropertyName("type")]
        public GeometryType Type => GeometryType.Polygon;
        
        [JsonPropertyName("coordinates")]
        public List<LineString> Coordinates { get; set; }

        [JsonIgnore]
        public bool IsRoot { get; set; }

        public bool Validate()
        {
            return 
                Coordinates != null
                && Coordinates.Any()
                && Coordinates.All(c => c.Validate());
        }
    }
}