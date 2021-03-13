using Xunit;
using BeerPartner.Domain.Entities;
using BeerPartner.Domain.Interfaces;
using System;

namespace BeerPartnerUnitTests.Domain
{
    public class PartnerTest
    {
        [Fact]
        public void Should_ValidateAndReturnTrue_When_ObjectIsValid()
        {
            // Arrange
            IValidatable partner = new Partner 
            {
                Id = Guid.NewGuid(),
                TradingName = "Adega do Zé",
                OwnerName = "José"
            };

            // Act
            bool result = partner.Validate();
            
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Should_ValidateAndReturnFalse_When_ObjectIsInvalid()
        {
            // Arrange
            IValidatable partner = new Partner 
            {
                Id = Guid.NewGuid(),
                TradingName = "Adega do Zé",
                OwnerName = "José"
            };

            // Act
            bool result = partner.Validate();
            
            // Assert
            Assert.False(result);
        }
    }
}