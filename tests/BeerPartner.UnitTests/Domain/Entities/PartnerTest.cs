using Xunit;
using BeerPartner.Domain.Interfaces;

namespace BeerPartner.UnitTests.Domain.Entities
{
    public class PartnerTest
    {
        [Fact]
        public void Should_ValidateAndReturnTrue_When_ObjectIsValid()
        {
            // Arrange
            IValidatable partner = PartnerDefaults.ValidPartner;

            // Act
            bool result = partner.Validate();
            
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Should_ValidateAndReturnFalse_When_ObjectIsInvalid()
        {
            // Arrange
            IValidatable partner = PartnerDefaults.InvalidPartner;

            // Act
            bool result = partner.Validate();
            
            // Assert
            Assert.False(result);
        }
    }
}