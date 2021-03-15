using System;
using BeerPartner.Domain.Entities;

namespace BeerPartner.Domain.Interfaces.Repositories
{
    public interface IPartnerRepository : IRepository<Partner, Guid>
    {
        Partner GetNearest(double longitude, double latitude);
    }
}