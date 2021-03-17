using BeerPartner.Infrastructure;
using BeerPartner.Infrastructure.Repositories;
using Xunit;
using System.Threading;
using Moq;
using System.Threading.Tasks;
using System;
using BeerPartner.Domain.Entities;
using BeerPartner.UnitTests.Domain.Entities;
using System.Collections.Generic;
using MongoDB.Driver;

namespace BeerPartner.UnitTests.Infrastructure.Repositories
{
    public class PartnerRepositoryTest
    {
        private Mock<IMongoClient> GetMockMongoClient(Mock<IMongoCollection<Partner>> collectionMock)
        {
            var databaseMock = new Mock<IMongoDatabase>();
            databaseMock.Setup(d => d.GetCollection<Partner>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
                .Returns(() => collectionMock.Object);

            var contextMock = new Mock<IMongoClient>();
            contextMock.Setup(c => c.GetDatabase(It.IsAny<string>(), It.IsAny<MongoDatabaseSettings>()))
                .Returns(() => databaseMock.Object);

            return contextMock;
        }

        [Fact]
        public void Should_CallConstructorWithoutErrors_When_RepositoryIsInitialized()
        {
            // Arrange
            var collectionMock = new Mock<IMongoCollection<Partner>>();
            var contextMock = GetMockMongoClient(collectionMock);
            var context = new MongoContextFaker(contextMock);

            // Act
            var repository = new PartnerRepository(context);

            // Assert
            Assert.NotNull(repository);
        }

        // [Fact]
        // public void Should_ReturnCallFindMethod_When_FoundPartnerById()
        // {
        //     // Arrange
        //     Guid id = Guid.NewGuid();
        //     Partner partner = PartnerDefaults.ValidPartner;
        //     partner.Id = id;
            
        //     var collectionMock = new Mock<IMongoCollection<Partner>>();

        //     var contextMock = GetMockMongoClient(collectionMock);
            
        //     var context = new MongoContextFaker(contextMock);
        //     var repository = new PartnerRepository(context);
            
        //     // Act
        //     var result = repository.GetById(id);

        //     // Assert
        //     collectionMock.Verify(c => c.Find<Partner>(It.IsAny<FilterDefinition<Partner>>(), It.IsAny<FindOptions>()));
        // }

        // [Fact]
        // public void Should_ReturnNull_When_NotFoundPartnerById()
        // {
        //     // Arrange
        //     Guid id = Guid.NewGuid();

        //     var collectionMock = new Mock<IMongoCollection<Partner>>();

        //     var contextMock = GetMockMongoClient(collectionMock);
            
        //     var context = new MongoContextFaker(contextMock);
        //     var repository = new PartnerRepository(context);
            
        //     // Act
        //     var result = repository.GetById(id);

        //     // Assert
        //     Assert.Null(result);
        // }

        // [Fact]
        // public void Should_ReturnAValidGuid_When_InsertAPartner()
        // {
        //     // Arrange
        //     Partner partner = PartnerDefaults.ValidPartner;

        //     var collectionMock = new Mock<IMongoCollection<Partner>>();
        //     collectionMock.Setup(d => d.InsertOne(It.IsAny<Partner>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()))
        //         .Callback<Partner, InsertOneOptions, CancellationToken>((entity, opt, canToken) => entity.Id = Guid.NewGuid());

        //     var contextMock = GetMockMongoClient(collectionMock);

        //     var context = new MongoContextFaker(contextMock);
        //     var repository = new PartnerRepository(context);
            
        //     // Act
        //     var result = repository.Insert(partner);

        //     // Assert
        //     Assert.NotEqual(Guid.Empty, result);
        //     Assert.IsType<Guid>(result);
        // }        

        // [Fact]
        // public void Should_ReturnTheNearestPartner_When_ReceiveALocationUnderCoverage()
        // {
        //     // Arrange
        //     double longitude = 10, latitude = 10;

        //     var findFluentMock = new Mock<IFindFluent<Partner, Partner>>();
        //     findFluentMock.Setup(d => d.First(It.IsAny<CancellationToken>()))
        //         .Returns(() => PartnerDefaults.ValidPartner);

        //     var collectionMock = new Mock<IMongoCollection<Partner>>();
        //     collectionMock.Setup(d => d.Find<Partner>(It.IsAny<FilterDefinition<Partner>>(), It.IsAny<FindOptions>()))
        //         .Returns(() => findFluentMock.Object);

        //     var contextMock = GetMockMongoClient(collectionMock);

        //     var context = new MongoContextFaker(contextMock);
        //     var repository = new PartnerRepository(context);
            
        //     // Act
        //     var result = repository.GetNearest(longitude, latitude);

        //     // Assert
        //     Assert.NotNull(result);
        // }

        // [Fact]
        // public void Should_ReturnNull_When_ReceiveALocationOutOfCoverage()
        // {
        //     // Arrange
        //     double longitude = 10, latitude = 10;

        //     var collectionMock = new Mock<IMongoCollection<Partner>>();

        //     var contextMock = GetMockMongoClient(collectionMock);

        //     var context = new MongoContextFaker(contextMock);
        //     var repository = new PartnerRepository(context);
            
        //     // Act
        //     var result = repository.GetNearest(longitude, latitude);

        //     // Assert
        //     Assert.Null(result);
        // }
    }
}