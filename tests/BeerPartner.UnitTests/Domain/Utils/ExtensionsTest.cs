using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Xunit;
using BeerPartner.Domain.Utils;

namespace BeerPartner.UnitTests.Domain.Utils
{
    public class ExtensionsTest
    {
        [Fact]
        public void Should_ReturnPropertyNameFromAttribute_When_ReceiveAPropertyWithJsonPropertyNameAttribute()
        {
            // Arrange
            string expectedValue = "fullName";
            var instance = new PropertyNameTest();

            // Act
            string result = Extensions.GetJsonPropertyName(instance, o => o.Name);
            
            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Should_ReturnPropertyName_When_ReceiveAPropertyWithoutJsonPropertyNameAttribute()
        {
            // Arrange
            string expectedValue = "Age";
            var instance = new PropertyNameTest();

            // Act
            string result = Extensions.GetJsonPropertyName(instance, o => o.Age);
            
            // Assert
            Assert.Equal(expectedValue, result);
        }

        [Fact]
        public void Should_ReturnThatClassIsAnIEnumerable_When_ReceiveAClassThatIsOrImplementsIEnumerable()
        {
            // Arrange
            var type = typeof(IEnumerableClassTest);

            // Act
            bool result = Extensions.IsIEnumerable(type);
            
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Should_ReturnThatClassIsNotAnIEnumerable_When_ReceiveAClassThatNotImplementsIEnumerable()
        {
            // Arrange
            var type = typeof(PropertyNameTest);

            // Act
            bool result = Extensions.IsIEnumerable(type);
            
            // Assert
            Assert.False(result);
        }
    }

    class PropertyNameTest
    {
        [JsonPropertyName("fullName")]
        public string Name { get; set; }

        public string Age { get; set; }
    }

    class IEnumerableClassTest : IEnumerable<object>
    {
        public IEnumerator<object> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}