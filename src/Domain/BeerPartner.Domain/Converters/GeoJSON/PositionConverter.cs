using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using BeerPartner.Domain.ValueObjects.GeoJSON;

namespace BeerPartner.Domain.Converters.GeoJSON
{
    public class PositionConverter : JsonConverter<Position>
    {
        public override Position Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if(reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException("Invalid JSON body.");

            using var jsonDocument = JsonDocument.ParseValue(ref reader);
            var values = jsonDocument.RootElement.EnumerateArray();

            if(!values.Any() || values.Count() < 2)
                throw new JsonException("Invalid position.");

            try
            {
                double longitude = values.ElementAt(0).GetDouble();
                double latitude = values.ElementAt(1).GetDouble();
                double? altitude = null;
                
                if(values.Count() > 2)
                    altitude = values.ElementAt(2).GetDouble();

                return new Position(longitude, latitude, altitude);
            } 
            catch (InvalidOperationException)
            {
                throw new JsonException("Invalid position data type.");
            }
        }

        public override void Write(Utf8JsonWriter writer, Position position, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            
            foreach(double value in position)
                JsonSerializer.Serialize(writer, value, options);

            writer.WriteEndArray();
        }
    }
}