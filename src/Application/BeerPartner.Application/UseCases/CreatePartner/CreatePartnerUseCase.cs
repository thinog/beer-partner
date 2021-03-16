using System;
using BeerPartner.Application.Interfaces;
using BeerPartner.Application.Interfaces.Repositories;
using BeerPartner.Domain.Entities;

namespace BeerPartner.Application.UseCases.CreatePartner
{
    public class CreatePartnerUseCase : ICreatePartnerUseCase
    {
        private IPartnerRepository _repository;
        private IOutputPort _outputPort;
        private ILogger _logger;

        public CreatePartnerUseCase(IPartnerRepository repository, ILogger logger)
        {
            _repository = repository;
            _outputPort = new DefaultOutputPort();
            _logger = logger;
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

                _logger.Info($"Inserting partner...");

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