using Amazon.DynamoDBv2.DataModel;
using System;

namespace BeerPartner.UnitTests.Infrastructure.Repositories
{
    internal static class AsyncSearchFaker<T>
    {
        public static AsyncSearch<T> GetInstance()
        {
            return (AsyncSearch<T>)Activator.CreateInstance(typeof(AsyncSearch<T>));
        }
    }
}