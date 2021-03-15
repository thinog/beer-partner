using System;
using BeerPartner.Domain.Entities;
using BeerPartner.Domain.Interfaces.Repositories;

namespace BeerPartner.Infrastructure
{
    public sealed class PartnerRepository : Repository<Partner, Guid>, IPartnerRepository
    {
        public Partner GetNearest(double longitude, double latitude)
        {
            throw new NotImplementedException();
        }
    }
}