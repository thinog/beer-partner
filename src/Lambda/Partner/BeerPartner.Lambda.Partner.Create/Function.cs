using System;
using System.Net;
using System.Text.Json;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using BeerPartner.Application.Interfaces.Repositories;
using BeerPartner.Application.UseCases.CreatePartner;
using BeerPartner.Infrastructure.IoC;
using BeerPartner.Infrastructure.Utils;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace BeerPartner.Lambda.Partner.Create
{
    public class Function : BaseLambda, IOutputPort
    {
        private APIGatewayProxyResponse _response;
        private ICreatePartnerUseCase _useCase;

        protected override void Setup(ILambdaContext context)
        {
            base.Setup(context);

            var partnerRepository = (IPartnerRepository)ServiceProvider.GetService(typeof(IPartnerRepository));
            
            _useCase = new CreatePartnerUseCase(partnerRepository, Logger);
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

                var partner = JsonSerializer.Deserialize<CreatePartnerRequest>(request.Body);
                _useCase.Execute(partner);
            }
            catch (JsonException exception)
            {
                Logger.Error(exception.Message, exception);
                Invalid(exception.Message);
            }
            catch (Exception exception)
            {
                Logger?.Error(exception.Message, exception);
                Error("An unexpected error has occured.");
            }

            return _response;
        }

        public void Ok(Guid id)
        {   
            Logger.Info($"Partner created with id {id}");
            _response = LambdaUtils.CreateResponse(new { id }, HttpStatusCode.OK);
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
    }
}
