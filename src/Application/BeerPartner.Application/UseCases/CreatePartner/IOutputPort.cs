using System;

namespace BeerPartner.Application.UseCases.CreatePartner
{
    public interface IOutputPort
    {
        void Ok(Guid id);
        void Invalid(string error);
        void Error(string error);
    }
}