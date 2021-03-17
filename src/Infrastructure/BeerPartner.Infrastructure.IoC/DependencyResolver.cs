using Microsoft.Extensions.DependencyInjection;
using BeerPartner.Domain.Interfaces.Repositories.Context;
using BeerPartner.Infrastructure.Repositories;
using BeerPartner.Infrastructure.Repositories.Context;
using BeerPartner.Application.UseCases.CreatePartner;
using BeerPartner.Application.UseCases.GetPartner;
using BeerPartner.Application.Interfaces.Repositories;
using System;
using BeerPartner.Application;
using BeerPartner.Application.Interfaces;
using MongoDB.Driver;

namespace BeerPartner.Infrastructure.IoC
{
    public static class DependencyResolver
    {
        public static IServiceProvider Resolve(IResolverConfiguration configuration = null)
        {
            var service = new ServiceCollection();

            return Configure(service, configuration)
                .BuildServiceProvider();
        }

        private static IServiceCollection Configure(IServiceCollection service, IResolverConfiguration configuration)
        {
            service.AddTransient(typeof(IDbContext<IMongoClient>), configuration?.DbContext ?? typeof(MongoContext));
            service.AddScoped(typeof(IPartnerRepository), configuration?.PartnerRepository ?? typeof(PartnerRepository));

            service.AddScoped(typeof(ICreatePartnerUseCase), configuration?.CreatePartnerUseCase ?? typeof(CreatePartnerUseCase));
            service.AddScoped(typeof(IGetPartnerUseCase), configuration?.GetPartnerUseCase ?? typeof(GetPartnerUseCase));

            service.AddSingleton(typeof(ILogger), configuration?.Logger ?? typeof(Logger));

            return service;
        }
    }
}