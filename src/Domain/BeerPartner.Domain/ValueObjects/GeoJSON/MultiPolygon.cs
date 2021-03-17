using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using BeerPartner.Domain.Converters.GeoJSON;
using BeerPartner.Domain.Enums;
using BeerPartner.Domain.Interfaces.ValueObjects;

namespace BeerPartner.Domain.ValueObjects.GeoJSON
{
    [JsonConverter(typeof(GeometryConverter<MultiPolygon, List<Polygon>>))]
    // https://tools.ietf.org/html/rfc7946#section-3.1.7
    public class MultiPolygon : IGeometry<List<Polygon>>
    {
        public MultiPolygon() { }

        [JsonPropertyName("type")]
        public GeometryType Type => GeometryType.MultiPolygon;
        
        [JsonPropertyName("coordinates")]
        public List<Polygon> Coordinates { get; set; }

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