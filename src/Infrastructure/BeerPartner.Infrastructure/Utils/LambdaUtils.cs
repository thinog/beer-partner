using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;

namespace BeerPartner.Infrastructure.Utils
{
    public class LambdaUtils
    {
        public static APIGatewayProxyResponse CreateResponse(
            object data, 
            HttpStatusCode status, 
            bool success = true, 
            string error = null)
        {
            var body = new  { success, data, error };

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)status,
                Body = JsonSerializer.Serialize(body),
                Headers = new Dictionary<string, string>
                {
                    {"Content-Type", "application/json"}
                }
            };
        }
    }
}