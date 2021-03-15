using System;

namespace BeerPartner.Application.UseCases.CreatePartner
{
    public class DefaultOutputPort : IOutputPort
    {
        public Guid Id { get; set; }
        public string ErrorMessage { get; set; }

        public void Ok(Guid id) => Id = id;
        public void Invalid(string error) => ErrorMessage = error;
        public void Error(string error) => ErrorMessage = error;
    }
}