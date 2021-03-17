using Moq;
using BeerPartner.Domain.Interfaces.Repositories.Context;
using MongoDB.Driver;
using BeerPartner.Domain.Entities;

namespace BeerPartner.UnitTests.Infrastructure.Repositories
{
    public class MongoContextFaker : IDbContext<IMongoClient>
    {
        private IMongoClient _connection;
        private string _databaseName;

        public IMongoClient Connection => _connection;
        public string DatabaseName => _databaseName;
        
        public static string ConnectionToStringMessage => "Dependency injected successfully! :D";


        public MongoContextFaker(Mock<IMongoClient> mock)
        {            
            Setup(mock);
        }

        public MongoContextFaker()
        {
            var mock = new Mock<IMongoClient>();
            mock.Setup(m => m.ToString()).Returns(() => ConnectionToStringMessage);

             Setup(mock);
        }

        private void Setup(Mock<IMongoClient> clientMock)
        {
            _connection = clientMock.Object;
            _databaseName = "test";
        }
    }
}