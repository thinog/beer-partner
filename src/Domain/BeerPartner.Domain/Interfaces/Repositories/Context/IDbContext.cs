namespace BeerPartner.Domain.Interfaces.Repositories.Context
{
    public interface IDbContext<TConnection>
    {
          TConnection Connection { get; }
          string DatabaseName { get; }
    }
}