using System.Collections.Generic;
using System.Linq;
using System.Net;
using BeerPartner.Infrastructure.Utils;
using Xunit;

namespace BeerPartner.UnitTests.Infrastructure.Utils
{
    public class LambdaUtilsTest
    {
        [Fact]
        public void Should_CreateAnHttpValidResponse_When_ReceiveASuccessObject()
        {
            // Arrange
            var requestBody = new { message = "success!" };
            var requestStatus = HttpStatusCode.OK;

            int expectedStatus = (int)HttpStatusCode.OK;
            string expectedBody = "{\"success\":true,\"data\":{\"message\":\"success!\"},\"error\":null}";
            var expectedHeader = new KeyValuePair<string, string>("Content-Type", "application/json");

            // Act
            var result = LambdaUtils.CreateResponse(requestBody, requestStatus);

            // Assert
            Assert.Equal(expectedStatus, result.StatusCode);
            Assert.Equal(expectedBody, result.Body);
            Assert.Equal(expectedHeader, result.Headers.FirstOrDefault());
        }

        [Fact]
        public void Should_CreateAnHttpValidResponse_When_ReceiveAnErrorObject()
        {
            // Arrange
            var requestBody = new { message = "error!" };
            var requestErrorMessage = "failed";
            var requestStatus = HttpStatusCode.InternalServerError;

            int expectedStatus = (int)HttpStatusCode.InternalServerError;
            string expectedBody = "{\"success\":false,\"data\":{\"message\":\"error!\"},\"error\":\"failed\"}";
            var expectedHeader = new KeyValuePair<string, string>("Content-Type", "application/json");

            // Act
            var result = LambdaUtils.CreateResponse(requestBody, requestStatus, false, requestErrorMessage);

            // Assert
            Assert.Equal(expectedStatus, result.StatusCode);
            Assert.Equal(expectedBody, result.Body);
            Assert.Equal(expectedHeader, result.Headers.FirstOrDefault());
        }
    }
}