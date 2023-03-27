namespace SuperCarga.Application.Domain.Drivers.Common.Abstraction
{
    public interface IDriversService
    {
        bool DriverExists(Guid id);

        Task UpdateDriverRates(Guid id, bool save);
    }
}
