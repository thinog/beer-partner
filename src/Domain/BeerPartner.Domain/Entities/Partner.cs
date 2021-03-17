using BeerPartner.Domain.ValueObjects.GeoJSON;
using BeerPartner.Domain.Interfaces;
using BeerPartner.Domain.Entities.Base;
using System;

namespace BeerPartner.Domain.Entities
{
    public class Partner : BaseEntity<Guid>, IValidatable
    {        
        public string TradingName { get; set; }
        
        public string OwnerName { get; set; }
        
        public MultiPolygon CoverageArea { get; set; }
        
        public Point Address { get; set; }

        public bool Validate()
        {
            return 
                !string.IsNullOrWhiteSpace(TradingName)
                && !string.IsNullOrWhiteSpace(OwnerName)
                && CoverageArea.Validate()
                && Address.Validate();
        }
    }
}