using Amazon.DynamoDBv2;
using Moq;
using BeerPartner.Domain.Interfaces.Repositories.Context;

namespace BeerPartner.UnitTests.Infrastructure.Repositories
{
    public class DynamoContextFaker : IDbContext<IAmazonDynamoDB>
    {
        public IAmazonDynamoDB Connection { get; }

        public DynamoContextFaker(Mock<IAmazonDynamoDB> mock)
        {            
            Connection = mock.Object;
        }
    }
}