using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using BeerPartner.Application.UseCases.GetPartner;
using BeerPartner.Infrastructure.Utils;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace BeerPartner.Lambda.Partner.Search
{
    public class Function : IOutputPort
    {
        private APIGatewayProxyResponse _response;
        private IGetPartnerUseCase _useCase;

        public Function()
        {
            _useCase = new GetPartnerUseCase(null);
            _useCase.SetOutputPort(this);
        }

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                if(request.PathParameters != null 
                    && request.PathParameters.ContainsKey("longitude")
                    && request.PathParameters.ContainsKey("latitude"))
                {
                    string @long = request.PathParameters["longitude"];
                    string lat = request.PathParameters["latitude"];

                    if(double.TryParse(@long, out double longitude)
                        && double.TryParse(lat, out double latitude))
                    {
                        _useCase.Search(longitude, latitude);
                    }
                    else
                    {
                        Invalid("Invalid longitude/latitude");
                    }                    
                }
                else
                {
                    Error("Longitude/latitude params are missing.");
                }                
            }
            catch (Exception)
            {
                Error("An unexpected error has occured.");
            }

            return _response;
        }

        public void Ok(GetPartnerResponse partner)
        {      
            _response = LambdaUtils.CreateResponse(partner, HttpStatusCode.OK);
        }

        public void Error(string error)
        {
            _response = LambdaUtils.CreateResponse(null, HttpStatusCode.InternalServerError, false, error);
        }

        public void Invalid(string error)
        {
            _response = LambdaUtils.CreateResponse(null, HttpStatusCode.BadRequest, false, error);
        }

        public void NotFound()
        {
            _response = LambdaUtils.CreateResponse(null, HttpStatusCode.NotFound, false, "Location out of coverage.");
        }
    }
}
