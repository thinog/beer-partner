using System;

namespace BeerPartner.Application.UseCases.GetPartner
{
    public interface IGetPartnerUseCase
    {
        void Get(Guid id);
        void Search(double longitude, double latitude);
        void SetOutputPort(IOutputPort outputPort);
    }
}