using System.Globalization;
using System.Text;
using System.Text.Json;
using BeerPartner.Domain.Converters.GeoJSON;
using BeerPartner.Domain.ValueObjects.GeoJSON;
using Xunit;
using Moq;
using System.Threading;

namespace BeerPartnerUnitTests.Domain.Converters.GeoJSON
{
    public class GeometryConverterTest
    {
        public GeometryConverterTest()
        {
            // Prevents double to string convertion to apply culture from host
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        [Fact]
        public void Should_ConvertJsonToPointObject_When_ReceiveAValidPointGeoJson()
        {
            // Arrange
            var converter = new GeometryConverter<Point, Position>();
            double longitude = -46.57421, latitude = -21.785741;
            string json = $"{{\"type\":\"Point\",\"coordinates\": [{longitude}, {latitude}]}}";
            
            // Act
            Point point = JsonSerializer.Deserialize<Point>(json);

            // Assert
            Assert.True(point.IsRoot);
            Assert.Equal(point.Coordinates.Longitude, longitude);
            Assert.Equal(point.Coordinates.Latitude, latitude);
        }

        [Fact]
        public void Should_ConvertGetAltitude_When_ReceiveAValidPointGeoJsonWithAltitude()
        {
            // Arrange
            var converter = new GeometryConverter<Point, Position>();
            double longitude = -46.57421, latitude = -21.785741, altitude = 789;
            string json = $"{{\"type\":\"Point\",\"coordinates\": [{longitude}, {latitude}, {altitude}]}}";
            
            // Act
            Point point = JsonSerializer.Deserialize<Point>(json);

            // Assert
            Assert.Equal(point.Coordinates.Altitude, altitude);
        }
        
        [Fact]
        public void Should_ThrowAnJsonException_When_NotReceiveLatitude()
        {
            // Arrange
            var converter = new GeometryConverter<Point, Position>();
            double longitude = -46.57421;
            string json = $"{{\"type\":\"Point\",\"coordinates\": [{longitude}]}}";
            
            // Act
            var exception = Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Point>(json));

            // Assert
            Assert.Equal("Invalid position.", exception.Message);
        }
        
        [Fact]
        public void Should_ReturnThatIsInvalid_When_ReceiveAnInvalidPosition()
        {
            // Arrange
            var converter = new GeometryConverter<Point, Position>();
            double longitude = -190, latitude = 100;
            string json = $"{{\"type\":\"Point\",\"coordinates\": [{longitude}, {latitude}]}}";
            
            // Act
            Point point = JsonSerializer.Deserialize<Point>(json);

            // Assert
            Assert.False(point.Validate());
        }
        
        [Fact]
        public void Should_ThrowAnJsonException_When_ReceiveAPointWithWrongType()
        {
            // Arrange
            var converter = new GeometryConverter<Point, Position>();
            double longitude = -46.57421, latitude = -21.785741;
            string json = $"{{\"type\":\"Polygon\",\"coordinates\": [{longitude}, {latitude}]}}";
            
            // Act
            var exception = Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Point>(json));

            // Assert
            Assert.Equal("Unexpected geometry type.", exception.Message);
        }
    }
}