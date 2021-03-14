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
    public class PositionConverterTest
    {
        public PositionConverterTest()
        {
            // Prevents double to string convertion to apply culture from host
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        [Fact]
        public void Should_ConvertJsonToPositionObject_When_ReceiveAValidPosition()
        {
            // Arrange
            double longitude = -46.57421, latitude = -21.785741;
            string json = $"[{longitude}, {latitude}]";
            
            // Act
            Position position = JsonSerializer.Deserialize<Position>(json);

            // Assert
            Assert.True(position.Validate());
            Assert.Equal(position.Longitude, longitude);
            Assert.Equal(position.Latitude, latitude);
        }

        [Fact]
        public void Should_GetAltitude_When_ReceiveAValidPositionWithAltitude()
        {
            // Arrange
            double altitude = 1234;
            string json = $"[0, 0, {altitude}]";
            
            // Act
            Position position = JsonSerializer.Deserialize<Position>(json);

            // Assert
            Assert.Equal(altitude, position.Altitude);
        }

        [Fact]
        public void Should_ReturnThatPositionIsInvalid_When_ReceiveAPositionOutOfRange()
        {
            // Arrange
            string json = "[1234, 1234]";
            
            // Act
            Position position = JsonSerializer.Deserialize<Position>(json);

            // Assert
            Assert.False(position.Validate());
        }

        [Fact]
        public void Should_ThrowAnJsonException_When_ReceiveAInvalidPosition()
        {
            // Arrange
            string json = "[\"ABC\"]";
            
            // Act
            var exception = Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Position>(json));

            // Assert
            Assert.Equal("Invalid position.", exception.Message);
        }
    }
}