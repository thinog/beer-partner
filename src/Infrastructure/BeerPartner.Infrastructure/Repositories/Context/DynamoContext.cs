using Amazon.DynamoDBv2;
using BeerPartner.Domain.Interfaces.Repositories.Context;
using BeerPartner.Domain.Utils;

namespace BeerPartner.Infrastructure.Repositories.Context
{
    public class DynamoContext : IDbContext<IAmazonDynamoDB>
    {
        public IAmazonDynamoDB Connection { get; }

        public DynamoContext()
        {
            AmazonDynamoDBConfig config = new AmazonDynamoDBConfig();
            
            string host = Configuration.Get("DynamoDb.Host");
            
            if(!string.IsNullOrWhiteSpace(host))
                config.ServiceURL = host;

            Connection = new AmazonDynamoDBClient(config);
        }

    }
}