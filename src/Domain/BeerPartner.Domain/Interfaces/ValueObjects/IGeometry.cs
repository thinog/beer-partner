using System.Collections.Generic;
using BeerPartner.Domain.Enums;

namespace BeerPartner.Domain.Interfaces.ValueObjects
{
    // https://tools.ietf.org/html/rfc7946#section-3.1
    public interface IGeometry<T>
    {
        GeometryType Type { get; }

        IEnumerable<T> Coordinates { get; set; }
    }
}