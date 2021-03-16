using System.Collections.Generic;
using Xunit;
using BeerPartner.Domain.ValueObjects.GeoJSON;

namespace BeerPartner.UnitTests.Domain.ValueObjects
{
    public class PositionTest
    {
        [Fact]
        public void Should_ReturnThatIsValid_When_ReceiveAValidPosition()
        {
            // Arrange
            var position = new Position(10, 10);

            // Act
            bool result = position.Validate();
            
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Should_ReturnThatIsInvalid_When_ReceiveAnInvalidPosition()
        {
            // Arrange
            var position = new Position(200, 100);

            // Act
            bool result = position.Validate();
            
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Should_ReturnAList_When_PassThroughAnIterator()
        {
            // Arrange
            var position = new Position(10, 30);
            var expectedValue = new List<double> { 10, 30 };
            var result = new List<double>();

            // Act
            foreach(double p in position)
                result.Add(p);
            
            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Should_ReturnAListWithAltitude_When_PassThroughAnIterator()
        {
            // Arrange
            var position = new Position(10, 30, 50);
            var expectedValue = new List<double> { 10, 30, 50 };
            var result = new List<double>();

            // Act
            foreach(double p in position)
                result.Add(p);
            
            // Assert
            Assert.Equal(expectedValue, result);
        }
    }
}