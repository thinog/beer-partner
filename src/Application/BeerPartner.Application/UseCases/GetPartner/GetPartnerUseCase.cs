using System;
using BeerPartner.Domain.Entities;
using BeerPartner.Domain.Interfaces.Repositories;
using BeerPartner.Domain.ValueObjects.GeoJSON;

namespace BeerPartner.Application.UseCases.GetPartner
{
    public class GetPartnerUseCase : IGetPartnerUseCase
    {
        private IPartnerRepository _repository;
        private IOutputPort _outputPort;

        public GetPartnerUseCase(IPartnerRepository repository)
        {
            _repository = repository;
            _outputPort = new DefaultOutputPort();
        }

        public void SetOutputPort(IOutputPort outputPort) => _outputPort = outputPort;

        public void Get(Guid id)
        {
            try
            {
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