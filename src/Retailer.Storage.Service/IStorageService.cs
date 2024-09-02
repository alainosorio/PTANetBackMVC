namespace Retailer.Storage.Service;

public interface IStorageService
{
    Task Sync(IEnumerable<Client.Model.Retail> retails);
}
