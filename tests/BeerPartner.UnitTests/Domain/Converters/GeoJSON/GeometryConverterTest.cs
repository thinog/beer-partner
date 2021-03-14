using System.Globalization;
using System.Text;
using System.Text.Json;
using BeerPartner.Domain.Converters.GeoJSON;
using BeerPartner.Domain.ValueObjects.GeoJSON;
using Xunit;
using Moq;
using System.Threading;
using System.Linq;
using BeerPartner.Domain.Enums;

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
            double altitude = 789;
            string json = $"{{\"type\":\"Point\",\"coordinates\": [0, 0, {altitude}]}}";
            
            // Act
            Point point = JsonSerializer.Deserialize<Point>(json);

            // Assert
            Assert.Equal(point.Coordinates.Altitude, altitude);
        }

        [Fact]
        public void Should_ConvertJsonToMultiPolygonObject_When_ReceiveAValidMultiPolygonGeoJson()
        {
            // Arrange
            var converter = new GeometryConverter<Point, Position>();
            double lastLongitude = 15, lastLatitude = 5;
            string json = $@"
                {{ 
                    ""type"": ""MultiPolygon"", 
                    ""coordinates"": [
                        [[[30, 20], [45, 40], [10, 40], [30, 20]]], 
                        [[[15, 5], [40, 10], [10, 20], [5, 10], [{lastLongitude}, {lastLatitude}]]]
                    ]
                }}";
            
            // Act
            MultiPolygon multiPolygon = JsonSerializer.Deserialize<MultiPolygon>(json);

            // Assert
            Assert.True(multiPolygon.IsRoot);
            Assert.Equal(GeometryType.MultiPolygon, multiPolygon.Type);

            Assert.False(multiPolygon.Coordinates.First().IsRoot);
            Assert.Equal(GeometryType.Polygon, multiPolygon.Coordinates.First().Type);
            
            Assert.False(multiPolygon.Coordinates.First().Coordinates.First().IsRoot);
            Assert.Equal(GeometryType.LineString, multiPolygon.Coordinates.First().Coordinates.First().Type);
            
            Assert.False(multiPolygon.Coordinates.First().Coordinates.First().Coordinates.First().IsRoot);
            Assert.Equal(GeometryType.Point, multiPolygon.Coordinates.First().Coordinates.First().Coordinates.First().Type);
            
            Assert.Equal(lastLongitude, multiPolygon.Coordinates.Last().Coordinates.Last().Coordinates.Last().Coordinates.Longitude);
            Assert.Equal(lastLatitude, multiPolygon.Coordinates.Last().Coordinates.Last().Coordinates.Last().Coordinates.Latitude);
        }
        
        [Fact]
        public void Should_ThrowAJsonException_When_NotReceiveLatitude()
        {
            // Arrange
            var converter = new GeometryConverter<Point, Position>();
            string json = "{\"type\":\"Point\",\"coordinates\": [0]}";
            
            // Act
            var exception = Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Point>(json));

            // Assert
            Assert.Equal("Invalid position.", exception.Message);
        }
        
        [Fact]
        public void Should_ReturnThatIsInvalid_When_ReceiveAPositionOutOfRange()
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
        public void Should_ThrowAnException_When_ReceiveAInvalidMultiPolygonGeoJson()
        {
            // Arrange
            var converter = new GeometryConverter<Point, Position>();
            string json = $@"
                {{ 
                    ""type"": ""MultiPolygon"", 
                    ""coordinates"": [[30, 20], [45, 40], [10, 40], [30, 20]]
                }}";
            
            // Act
            var exception = Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<MultiPolygon>(json));

            // Assert
            Assert.Equal("Invalid JSON body.", exception.Message);
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
        
        [Fact]
        public void Should_ThrowAnJsonException_When_ReceiveAnInvalidJsonTokenType()
        {
            // Arrange
            var converter = new GeometryConverter<Point, Position>();
            var jsonBytes = Encoding.UTF8.GetBytes("{}");            
            
            // Act
            var exception = Assert.Throws<JsonException>(() => 
            {
                var utf8JsonReader = new Utf8JsonReader(jsonBytes, true, new JsonReaderState());
                converter.Read(ref utf8JsonReader, typeof(Point), new JsonSerializerOptions());
            });

            // Assert
            Assert.Equal("Invalid JSON body.", exception.Message);
        }
    }
}