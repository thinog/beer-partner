using System;

namespace BeerPartner.Application.Interfaces
{
    public interface ILogger
    {
        void Info(string message, object data = null);
        void Error(string message, Exception exception = null);
    }
}