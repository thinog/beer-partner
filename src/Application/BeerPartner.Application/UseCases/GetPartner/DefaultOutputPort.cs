namespace BeerPartner.Application.UseCases.GetPartner
{
    public class DefaultOutputPort : IOutputPort
    {
        public GetPartnerResponse Partner { get; set; }
        public string ErrorMessage { get; set; }

        public void Ok(GetPartnerResponse partner) => Partner = partner;
        public void Invalid(string error) => ErrorMessage = error;
        public void Error(string error) => ErrorMessage = error;
        public void NotFound() => ErrorMessage = "Not found.";
    }
}