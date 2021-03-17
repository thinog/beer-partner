using BeerPartner.Domain.Interfaces.Repositories.Context;
using BeerPartner.Infrastructure.IoC;
using BeerPartner.UnitTests.Infrastructure.Repositories;
using MongoDB.Driver;
using Xunit;

namespace BeerPartner.UnitTests.Infrastructure.IoC
{
    public class DependencyResolverTest
    {
        [Fact]
        public void Should_InvokeInterfaceRightMethod_When_DependencyResolverIsSet()
        {
            // Arrange
            var configuration = new ResolverConfigurationFaker();
            configuration.DbContext = typeof(MongoContextFaker);
                       
            // Act
            var service = DependencyResolver.Resolve(configuration);
            var dbContext = (IDbContext<IMongoClient>)service.GetService(typeof(IDbContext<IMongoClient>));

            // Act - Assert
            Assert.Equal(MongoContextFaker.ConnectionToStringMessage, dbContext.Connection.ToString());
        }
    }
}