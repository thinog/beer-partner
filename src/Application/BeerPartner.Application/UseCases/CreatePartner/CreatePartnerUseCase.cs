using System;
using BeerPartner.Domain.Entities;
using BeerPartner.Domain.Interfaces.Repositories;

namespace BeerPartner.Application.UseCases.CreatePartner
{
    public class CreatePartnerUseCase : ICreatePartnerUseCase
    {
        private IPartnerRepository _repository;
        private IOutputPort _outputPort;

        public CreatePartnerUseCase(IPartnerRepository repository)
        {
            _repository = repository;
            _outputPort = new DefaultOutputPort();
        }

        public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;

        public void Execute(CreatePartnerRequest request)
        {
            Partner partner = request?.ToPartner();

            try
            {
                if(!partner.Validate())
                {
                    _outputPort.Invalid("Invalid body.");
                    return;
                }

                partner.Id = _repository.Insert(partner);
                
                _outputPort.Ok(partner.Id);
            }
            catch(Exception ex)
            {
                _outputPort.Error(ex.Message);
            }
        }
    }
}