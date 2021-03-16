using Microsoft.Extensions.DependencyInjection;
using BeerPartner.Domain.Interfaces.Repositories.Context;
using BeerPartner.Infrastructure.Repositories.Context;
using Amazon.DynamoDBv2;
using BeerPartner.Application.UseCases.CreatePartner;
using BeerPartner.Application.UseCases.GetPartner;
using BeerPartner.Application.Interfaces.Repositories;
using System;
using BeerPartner.Application;
using BeerPartner.Application.Interfaces;

namespace BeerPartner.Infrastructure.IoC
{
    public static class DependencyResolver
    {
        public static IServiceProvider Resolve()
        {
            var service = new ServiceCollection();

            return Configure(service)
                .BuildServiceProvider();
        }

        private static IServiceCollection Configure(IServiceCollection service)
        {
            service.AddTransient<IDbContext<IAmazonDynamoDB>, DynamoContext>();
            service.AddScoped<IPartnerRepository, PartnerRepository>();

            service.AddScoped<ICreatePartnerUseCase, CreatePartnerUseCase>();
            service.AddScoped<IGetPartnerUseCase, GetPartnerUseCase>();

            service.AddSingleton<ILogger, Logger>();

            return service;
        }
    }
}