using System;

namespace BeerPartner.Application.UseCases.GetPartner
{
    public interface IOutputPort
    {
        void Ok(GetPartnerResponse partner);
        void Invalid(string error);
        void Error(string error);
        void NotFound();
    }
}