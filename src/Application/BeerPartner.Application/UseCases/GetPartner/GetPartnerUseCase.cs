using System;
using BeerPartner.Application.Interfaces;
using BeerPartner.Application.Interfaces.Repositories;
using BeerPartner.Domain.Entities;
using BeerPartner.Domain.ValueObjects.GeoJSON;

namespace BeerPartner.Application.UseCases.GetPartner
{
    public class GetPartnerUseCase : IGetPartnerUseCase
    {
        private IPartnerRepository _repository;
        private IOutputPort _outputPort;
        private ILogger _logger;

        public GetPartnerUseCase(IPartnerRepository repository, ILogger logger)
        {
            _repository = repository;
            _outputPort = new DefaultOutputPort();
            _logger = logger;
        }

        public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;

        public void Get(Guid id)
        {
            try
            {
                _logger.Info($"Getting partner id {id}...");
                
                Partner partner = _repository.GetById(id);

                if(partner == null)
                {
                    _outputPort.NotFound();
                    return;
                }
                
                var partnerResponse = new GetPartnerResponse(partner);
                _outputPort.Ok(partnerResponse);
            }
            catch(Exception ex)
            {
                _outputPort.Error(ex.Message);
            }
        }

        public void Search(double longitude, double latitude)
        {
            var position = new Position(longitude, latitude);

            if(!position.Validate())
            {
                _outputPort.Invalid("Invalid longitude/latitude");
                return;
            }

            try
            {
                _logger.Info($"Searching nearest partner from long {position.Longitude} / lat {position.Latitude}");

                Partner partner = _repository.GetNearest(position.Longitude, position.Latitude);

                if(partner == null)
                {
                    _outputPort.NotFound();
                    return;
                }
                
                var partnerResponse = new GetPartnerResponse(partner);
                _outputPort.Ok(partnerResponse);
            }
            catch(Exception ex)
            {
                _outputPort.Error(ex.Message);
            }
        }
    }
}