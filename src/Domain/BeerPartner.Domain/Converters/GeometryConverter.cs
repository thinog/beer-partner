using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BeerPartner.Domain.Converters
{
    public class GeometryConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            throw new NotImplementedException();
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}