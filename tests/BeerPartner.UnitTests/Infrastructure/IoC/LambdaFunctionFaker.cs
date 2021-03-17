using Amazon.Lambda.Core;
using BeerPartner.Infrastructure.IoC;

namespace BeerPartner.UnitTests.Infrastructure.IoC
{
    internal class LambdaFunctionFaker : BaseLambda
    {
        internal void SetupLambda(ILambdaContext context)
        {
            base.Setup(context);
        }
    }
}