using BeerPartner.Infrastructure;
using BeerPartner.Infrastructure.Repositories;
using Xunit;
using Amazon.DynamoDBv2.Model;
using System.Threading;
using Moq;
using Amazon.DynamoDBv2;
using System.Threading.Tasks;
using System;

namespace BeerPartner.UnitTests.Infrastructure.Repositories
{
    public class PartnerRepositoryTest
    {
        [Fact]
        public void Should_CreateAnHttpValidResponse_When_ReceiveASuccessObject()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            var contextMock = new Mock<IAmazonDynamoDB>();
            contextMock.Setup(m => m.GetItemAsync(It.IsAny<GetItemRequest>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.Run(() => new GetItemResponse()));
            contextMock.Setup(m => m.DescribeTableAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.Run(() => new DescribeTableResponse { Table = new TableDescription() }));
            
            var context = new DynamoContextFaker(contextMock);
            var repository = new PartnerRepository(context);
            
            // Act
            var result = repository.GetById(id);

            // Assert
            Assert.Null(result);
        }
    }
}