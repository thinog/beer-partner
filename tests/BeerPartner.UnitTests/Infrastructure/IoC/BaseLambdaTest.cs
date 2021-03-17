using System;
using Amazon.Lambda.Core;
using Moq;
using Xunit;

namespace BeerPartner.UnitTests.Infrastructure.IoC
{
    public class BaseLambdaTest
    {
        [Fact]
        public void Should_SetAspNetCoreEnvVar_When_CallSetupMethod()
        {
            // Arrange
            string expectedValue = "prd";
            var lambdaFaker = new LambdaFunctionFaker();
            var context = new Mock<ILambdaContext>();
            context.Setup(c => c.InvokedFunctionArn).Returns(":prd");
            
            // Act
            lambdaFaker.SetupLambda(context.Object);
            
            // Assert
            context.Verify(c => c.InvokedFunctionArn);
            Assert.Equal(expectedValue, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        }
    }
}