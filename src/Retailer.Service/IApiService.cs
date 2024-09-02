namespace Retailer.Service;

public interface IApiService
{
    Task<IEnumerable<Retail>> Get(string path);
}
