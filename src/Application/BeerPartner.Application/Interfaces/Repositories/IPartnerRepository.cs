using System;
using BeerPartner.Domain.Entities;
using BeerPartner.Domain.Interfaces.Repositories;

namespace BeerPartner.Application.Interfaces.Repositories
{
    public interface IPartnerRepository : IRepository<Partner, Guid>
    {
        Partner GetNearest(double longitude, double latitude);
    }
}