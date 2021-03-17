using BeerPartner.Infrastructure;
using BeerPartner.Infrastructure.Repositories;
using Xunit;
using Amazon.DynamoDBv2.Model;
using System.Threading;
using Moq;
using Amazon.DynamoDBv2;
using System.Threading.Tasks;
using System;
using Amazon.DynamoDBv2.DataModel;
using BeerPartner.Domain.Entities;
using BeerPartner.UnitTests.Domain.Entities;
using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;

namespace BeerPartner.UnitTests.Infrastructure.Repositories
{
    public class PartnerRepositoryTest
    {
        [Fact]
        public void Should_ReturnAValidPartner_When_FoundPartnerById()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Partner partner = PartnerDefaults.ValidPartner;
            partner.Id = id;

            var contextMock = new Mock<IDynamoDBContext>();
            contextMock.Setup(m => m.LoadAsync<Partner>(It.IsAny<object>(), It.IsAny<DynamoDBOperationConfig>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.Run(() => partner));
            
            var context = new DynamoContextFaker(contextMock);
            var repository = new PartnerRepository(context);
            
            // Act
            var result = repository.GetById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public void Should_ReturnNull_When_NotFoundPartnerById()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            var contextMock = new Mock<IDynamoDBContext>();
            contextMock.Setup(m => m.LoadAsync<Partner>(It.IsAny<object>(), It.IsAny<DynamoDBOperationConfig>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.Run(() => (Partner)null));
            
            var context = new DynamoContextFaker(contextMock);
            var repository = new PartnerRepository(context);
            
            // Act
            var result = repository.GetById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Should_ReturnAValidGuid_When_InsertAPartner()
        {
            // Arrange
            Partner partner = PartnerDefaults.ValidPartner;

            var contextMock = new Mock<IDynamoDBContext>();
            var context = new DynamoContextFaker(contextMock);
            var repository = new PartnerRepository(context);
            
            // Act
            var result = repository.Insert(partner);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            Assert.IsType<Guid>(result);
        }        

        // [Fact]
        // public void Should_ReturnTheNearestPartner_When_ReceiveALocationUnderCoverage()
        // {
        //     // Arrange
        //     double longitude = 10, latitude = 10;

        //     var contextMock = new Mock<IDynamoDBContext>();
        //     contextMock.Setup(c => c.ScanAsync<Partner>(It.IsAny<IEnumerable<ScanCondition>>(), It.IsAny<DynamoDBOperationConfig>()))
        //         .Returns(() => AsyncSearchFaker<Partner>.GetInstance());

        //     var context = new DynamoContextFaker(contextMock);
        //     var repository = new PartnerRepository(context);
            
        //     // Act
        //     var result = repository.Get(partner);

        //     // Assert
        //     Assert.NotEqual(Guid.Empty, result);
        //     Assert.IsType<Guid>(result);
        // }
    }
}