using BeerPartner.Domain.Enums;

namespace BeerPartner.Domain.Interfaces.ValueObjects
{
    // https://tools.ietf.org/html/rfc7946#section-3.1
    public interface IGeometry<TCoordinates> : IValidatable
    {
        GeometryType Type { get; }

        TCoordinates Coordinates { get; set; }

        bool IsRoot { get; set; }
    }
}