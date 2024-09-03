namespace Retailer.Storage.Service;

public interface IStorageService
{
    Task<Client.Model.Retail> GetRetailerById(string id);
    Task Sync(IEnumerable<Client.Model.Retail> retails);
}
