using System;
using Amazon.Lambda.Core;
using BeerPartner.Application.Interfaces;

namespace BeerPartner.Infrastructure.IoC
{
    public class BaseLambda
    {
        protected IServiceProvider ServiceProvider;
        protected ILogger Logger;

        protected virtual void Setup(ILambdaContext context)
        {
            if(context.InvokedFunctionArn.EndsWith(":prd"))
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "prd");
            else
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "dev");

            ServiceProvider = DependencyResolver.Resolve();
            
            Logger = (ILogger)ServiceProvider.GetService(typeof(ILogger));

            Logger.Info("Lambda base setup done.");
        }
    }
}