using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using BeerPartner.Domain.Enums;
using BeerPartner.Domain.Interfaces.ValueObjects;
using BeerPartner.Domain.Utils;

namespace BeerPartner.Domain.Converters.GeoJSON
{
    public class GeometryConverter<TGeometry, TCoordinates> : JsonConverter<TGeometry> where TGeometry : IGeometry<TCoordinates>, new()
    {

        public override TGeometry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            TGeometry geometry = reader.TokenType switch
            {
                JsonTokenType.StartObject => ReadObject(ref reader, options),
                JsonTokenType.StartArray => ReadArray(ref reader, options),
                _ => default
            };
            
            if(geometry is null)
                throw new JsonException("Invalid JSON body.");

            return geometry;
        }

        public override void Write(Utf8JsonWriter writer, TGeometry value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        private TGeometry ReadObject(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            var jsonDocument = JsonDocument.ParseValue(ref reader);

            var geometry = new TGeometry();
            string typePropertyName = geometry.GetJsonPropertyName(o => o.Type);
            string coordinatesPropertyName = geometry.GetJsonPropertyName(o => o.Coordinates);

            foreach(var prop in jsonDocument.RootElement.EnumerateObject())
            {
                if(prop.Name.Equals(typePropertyName, StringComparison.InvariantCultureIgnoreCase))
                {
                    if(!Enum.TryParse(typeof(GeometryType), prop.Value.GetString(), ignoreCase: true, out object type)
                        || (GeometryType)type != geometry.Type)
                    {
                        throw new JsonException("Unexpected geometry type.");
                    }

                    geometry.IsRoot = true;
                    continue;
                }

                if(prop.Name.Equals(coordinatesPropertyName, StringComparison.InvariantCultureIgnoreCase))
                {
                    geometry.Coordinates = JsonSerializer.Deserialize<TCoordinates>(prop.Value.GetRawText(), options);
                    continue;
                }
            }

            return geometry;
        }

        private TGeometry ReadArray(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            var jsonDocument = JsonDocument.ParseValue(ref reader);

            var geometry = new TGeometry();

            geometry.Coordinates = JsonSerializer.Deserialize<TCoordinates>(jsonDocument.RootElement.GetRawText(), options);

            return geometry;
        }
    }
}