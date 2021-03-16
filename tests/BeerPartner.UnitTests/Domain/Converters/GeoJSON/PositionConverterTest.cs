using System.Globalization;
using System.Text.Json;
using BeerPartner.Domain.ValueObjects.GeoJSON;
using Xunit;
using System.Threading;

namespace BeerPartner.UnitTests.Domain.Converters.GeoJSON
{
    public class PositionConverterTest
    {
        public PositionConverterTest()
        {
            // Prevents double to string convertion to apply culture from host
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        #region Read
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
        public void Should_ThrowAnJsonException_When_ReceiveAnInvalidPosition()
        {
            // Arrange
            string json = "[0]";
            
            // Act
            var exception = Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Position>(json));

            // Assert
            Assert.Equal("Invalid position.", exception.Message);
        }

        [Fact]
        public void Should_ThrowAnJsonException_When_ReceiveAnInvalidPositionDataType()
        {
            // Arrange
            string json = "[\"ABC\", \"DEF\", \"GHI\"]";
            
            // Act
            var exception = Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Position>(json));

            // Assert
            Assert.Equal("Invalid position data type.", exception.Message);
        }
        #endregion

        #region Write
        [Fact]
        public void Should_ConvertPositionObjectToJson_When_ReceiveAValidPositionObject()
        {
            // Arrange
            Position position = new Position
            {
                Longitude = 123.123,
                Latitude = 10
            };
            
            // Act
            string json = JsonSerializer.Serialize(position);

            // Assert
            Assert.True(!string.IsNullOrWhiteSpace(json));
            Assert.Equal("[123.123,10]", json);
        }

        [Fact]
        public void Should_ConvertPositionObjectToJson_When_ReceiveAValidPositionObjectWithAltitude()
        {
            // Arrange
            Position position = new Position
            {
                Longitude = 123.123,
                Latitude = 10,
                Altitude = 321
            };
            
            // Act
            string json = JsonSerializer.Serialize(position);

            // Assert
            Assert.True(!string.IsNullOrWhiteSpace(json));
            Assert.Equal("[123.123,10,321]", json);
        }
        #endregion
    }
}