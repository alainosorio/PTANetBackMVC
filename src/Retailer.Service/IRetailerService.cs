namespace Retailer.Service;

public interface IRetailerService
{
    Task<IEnumerable<Retail>> Sync();
    Task<Retail> GetRetailerById(string id);
}
