using System;
using BeerPartner.Domain.Utils;
using Xunit;

namespace BeerPartner.UnitTests.Domain.Utils
{
    public class ConfigurationTest
    {
        public ConfigurationTest()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "dev");
        }

        [Fact]
        public void Should_ReturnConfigurationValue_When_ReceiveAValidConfigurationKey()
        {
            // Arrange
            string key = "DynamoDb.Partner.TableName";
            string expectedValue = "test_beer_partner";

            // Act
            string result = Configuration.Get(key);
            
            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Should_ReturnAnEmptyString_When_ReceiveAnKeyThatNotExists()
        {
            // Arrange
            string key = "Random.Key";
            string expectedValue = string.Empty;

            // Act
            string result = Configuration.Get(key);
            
            // Assert
            Assert.Equal(expectedValue, result);
        }
    }
}