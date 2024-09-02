namespace Retailer.Service;

public interface IRetailerService
{
    Task<IEnumerable<Retail>> Sync();
    Task<IEnumerable<Retail>> CreateRetailer(IEnumerable<Retail> retailers);
    Task<Retail> GetRetailerById(int id);
}
