using Amazon.DynamoDBv2.DataModel;
using BeerPartner.Domain.Interfaces.Repositories.Context;
using BeerPartner.Infrastructure.IoC;
using BeerPartner.UnitTests.Infrastructure.Repositories;
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
            configuration.DbContext = typeof(DynamoContextFaker);
                       
            // Act
            var service = DependencyResolver.Resolve(configuration);
            var dbContext = (IDbContext<IDynamoDBContext>)service.GetService(typeof(IDbContext<IDynamoDBContext>));

            // Act - Assert
            Assert.Equal(DynamoContextFaker.ConnectionToStringMessage, dbContext.Connection.ToString());
        }
    }
}