namespace BeerPartner.Domain.ValueObjects.GeoJSON
{
    // https://tools.ietf.org/html/rfc7946#section-3.1.1
    public class Position
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public Position(decimal latitude, decimal longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}