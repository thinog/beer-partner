using System;

namespace BeerPartner.Infrastructure.IoC
{
    public interface IResolverConfiguration
    {
        Type DbContext { get; set; }
        Type PartnerRepository { get; set; }
        Type CreatePartnerUseCase { get; set; }
        Type GetPartnerUseCase { get; set; }
        Type Logger { get; set; }
    }
}