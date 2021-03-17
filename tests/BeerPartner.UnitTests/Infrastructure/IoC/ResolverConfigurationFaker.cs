using System;
using BeerPartner.Infrastructure.IoC;

namespace BeerPartner.UnitTests.Infrastructure.IoC
{
    internal class ResolverConfigurationFaker : IResolverConfiguration
    {
        public Type DbContext { get; set; }
        public Type PartnerRepository { get; set; }
        public Type CreatePartnerUseCase { get; set; }
        public Type GetPartnerUseCase { get; set; }
        public Type Logger { get; set; }
    }
}