using System;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using BeerPartner.Application.Interfaces.Repositories;
using BeerPartner.Application.UseCases.GetPartner;
using BeerPartner.Infrastructure.IoC;
using BeerPartner.Infrastructure.Utils;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace BeerPartner.Lambda.Partner.Search
{
    public class Function : BaseLambda, IOutputPort
    {
        private APIGatewayProxyResponse _response;
        private IGetPartnerUseCase _useCase;
        

        protected override void Setup(ILambdaContext context)
        {
            base.Setup(context);
            
            var partnerRepository = (IPartnerRepository)ServiceProvider.GetService(typeof(IPartnerRepository));            

            _useCase = new GetPartnerUseCase(partnerRepository, Logger);
            _useCase.SetOutputPort(this);

            Logger.Info("Lambda setup done.");
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
                Setup(context);

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
            catch (Exception exception)
            {
                Logger?.Error(exception.Message, exception);
                Error("An unexpected error has occured.");
            }

            return _response;
        }

        public void Ok(GetPartnerResponse partner)
        {     
            Logger.Info("Partner found", partner);
            _response = LambdaUtils.CreateResponse(partner, HttpStatusCode.OK);
        }

        public void Error(string error)
        {
            Logger.Error(error);
            _response = LambdaUtils.CreateResponse(null, HttpStatusCode.InternalServerError, false, error);
        }

        public void Invalid(string error)
        {
            Logger.Error($"Invalid request: {error}");
            _response = LambdaUtils.CreateResponse(null, HttpStatusCode.BadRequest, false, error);
        }

        public void NotFound()
        {
            Logger.Info($"No partner found");
            _response = LambdaUtils.CreateResponse(null, HttpStatusCode.NotFound, false, "Location out of coverage.");
        }
    }
}
