using System;
using System.Collections.Generic;
using BeerPartner.Domain.Entities;
using BeerPartner.Domain.ValueObjects.GeoJSON;

namespace BeerPartner.UnitTests.Domain.Entities
{
    internal static class PartnerDefaults
    {
        internal static Partner ValidPartner => new Partner 
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
                                                Longitude = 0,
                                                Latitude = 0
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
                        Longitude = 0,
                        Latitude = 0
                    }
                }
            };

        internal static Partner InvalidPartner => new Partner 
            {
                TradingName = "Adega do Zé",
                OwnerName = "",
                CoverageArea = new MultiPolygon(),
                Address = new Point()
            };
    }
}