using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using BeerPartner.Domain.Interfaces.Repositories.Context;
using BeerPartner.Domain.Utils;

namespace BeerPartner.Infrastructure.Repositories.Context
{
    public class DynamoContext : IDbContext<IDynamoDBContext>
    {
        public IDynamoDBContext Connection { get; }

        public DynamoContext()
        {
            AmazonDynamoDBConfig config = new AmazonDynamoDBConfig();
            
            string host = Configuration.Get("DynamoDb.Host");
            
            if(!string.IsNullOrWhiteSpace(host))
                config.ServiceURL = host;

            var client = new AmazonDynamoDBClient(config);

            Connection = new DynamoDBContext(client);
        }

    }
}