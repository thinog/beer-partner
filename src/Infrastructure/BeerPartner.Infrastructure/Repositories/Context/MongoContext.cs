using BeerPartner.Domain.Interfaces.Repositories.Context;
using BeerPartner.Domain.Utils;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace BeerPartner.Infrastructure.Repositories.Context
{
    public class MongoContext : IDbContext<IMongoClient>
    {
        public IMongoClient Connection { get; }

        public string DatabaseName { get; }

        public MongoContext()
        {
			RegisterConventions();

			var url = new MongoUrl(Configuration.Get("MongoDB.ConnectionString"));
			var settings = MongoClientSettings.FromUrl(url);
			Connection = new MongoClient(settings);

            DatabaseName = url.DatabaseName;
        }

        private void RegisterConventions()
		{
			var pack = new ConventionPack
			{
				new IgnoreExtraElementsConvention(true),
				new IgnoreIfDefaultConvention(true)
			};

			ConventionRegistry.Register("Beer Partner conventions :)", pack, t => true);
		}
    }
}