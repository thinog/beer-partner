using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using BeerPartner.Application.UseCases.CreatePartner;
using BeerPartner.Infrastructure.Utils;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace BeerPartner.Lambda.Partner.Create
{
    public class Function : IOutputPort
    {
        private APIGatewayProxyResponse _response;
        private ICreatePartnerUseCase _useCase;

        public Function()
        {
            _useCase = new CreatePartnerUseCase(null);
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
                var partner = JsonSerializer.Deserialize<CreatePartnerRequest>(request.Body);
                _useCase.Execute(partner);
            }
            catch (JsonException exception)
            {
                Error(exception.Message);
            }
            catch (Exception)
            {
                Error("An unexpected error has occured.");
            }

            return _response;
        }

        public void Ok(Guid id)
        {      
            _response = LambdaUtils.CreateResponse(new { id }, HttpStatusCode.OK);
        }

        public void Error(string error)
        {
            _response = LambdaUtils.CreateResponse(null, HttpStatusCode.InternalServerError, false, error);
        }

        public void Invalid(string error)
        {
            _response = LambdaUtils.CreateResponse(null, HttpStatusCode.BadRequest, false, error);
        }
    }
}
