using System.Text.Json.Serialization;
using BeerPartner.Domain.Converters.GeoJSON;
using BeerPartner.Domain.Interfaces;

namespace BeerPartner.Domain.ValueObjects.GeoJSON
{
    [JsonConverter(typeof(PositionConverter))]
    // https://tools.ietf.org/html/rfc7946#section-3.1.1
    public class Position : IValidatable
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Altitude { get; set; }
        
        public Position() { }

        public Position(double longitude, double latitude, double? altitude = null)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Altitude = altitude;
        }

        // https://tools.ietf.org/html/rfc5870#section-2
        public bool Validate()
        {
            return
                (Longitude >= -180 && Longitude <= 180)
                && (Latitude >= -90 && Longitude <= 90);
        }
    }
}