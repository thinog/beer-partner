using Moq;
using BeerPartner.Domain.Interfaces.Repositories.Context;
using Amazon.DynamoDBv2.DataModel;

namespace BeerPartner.UnitTests.Infrastructure.Repositories
{
    public class DynamoContextFaker : IDbContext<IDynamoDBContext>
    {
        public IDynamoDBContext Connection { get; }
        
        public static string ConnectionToStringMessage => "Dependency injected successfully! :D";

        public DynamoContextFaker(Mock<IDynamoDBContext> mock)
        {            
            Connection = mock.Object;
        }

        public DynamoContextFaker()
        {
            var mock = new Mock<IDynamoDBContext>();
            mock.Setup(m => m.ToString()).Returns(() => ConnectionToStringMessage);

            Connection = mock.Object;
        }
    }
}