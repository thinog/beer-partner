namespace BeerPartner.Application.UseCases.CreatePartner
{
    public interface ICreatePartnerUseCase
    {
        void Execute(CreatePartnerRequest request);
        void SetOutputPort(IOutputPort outputPort);
    }
}