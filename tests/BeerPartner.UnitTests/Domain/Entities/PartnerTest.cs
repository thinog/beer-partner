using Xunit;
using BeerPartner.Domain.Entities;
using BeerPartner.Domain.Interfaces;
using System;
using BeerPartner.Domain.ValueObjects.GeoJSON;
using System.Collections.Generic;

namespace BeerPartnerUnitTests.Domain.Entities
{
    public class PartnerTest
    {
        [Fact]
        public void Should_ValidateAndReturnTrue_When_ObjectIsValid()
        {
            // Arrange
            IValidatable partner = new Partner 
            {
                TradingName = "Adega do Zé",
                OwnerName = "José",
                CoverageArea = new MultiPolygon
                {
                    Coordinates = new List<Polygon>
                    {
                        new Polygon
                        {
                            Coordinates = new List<LineString>
                            {
                                new LineString
                                {
                                    Coordinates = new List<Point>
                                    {
                                        new Point
                                        {
                                            Coordinates = new Position
                                            {
                                                Altitude = 0,
                                                Longitude = 0
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                Address = new Point
                {
                    Coordinates = new Position
                    {
                        Altitude = 0,
                        Longitude = 0
                    }
                }
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
                OwnerName = "",
                CoverageArea = new MultiPolygon(),
                Address = new Point()
            };

            // Act
            bool result = partner.Validate();
            
            // Assert
            Assert.False(result);
        }
    }
}