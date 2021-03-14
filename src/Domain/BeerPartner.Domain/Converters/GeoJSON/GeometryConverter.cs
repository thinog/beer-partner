using System;
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
            var tokenType = reader.TokenType;

            if(tokenType != JsonTokenType.StartObject && tokenType != JsonTokenType.StartArray)
                throw new JsonException("Invalid JSON body.");

            using var jsonDocument = JsonDocument.ParseValue(ref reader);
            TGeometry geometry;

            geometry = tokenType == JsonTokenType.StartObject
                ? ReadObject(jsonDocument)
                : ReadArray(jsonDocument);

            return geometry;
        }

        public override void Write(Utf8JsonWriter writer, TGeometry value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        private TGeometry ReadObject(JsonDocument jsonDocument)
        {
            TGeometry geometry = new TGeometry();
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
                    geometry.Coordinates = JsonSerializer.Deserialize<TCoordinates>(prop.Value.ToString());
                    continue;
                }
            }

            return geometry;
        }

        private TGeometry ReadArray(JsonDocument jsonDocument)
        {
            TGeometry geometry = new TGeometry();

            return geometry;
        }
    }
}