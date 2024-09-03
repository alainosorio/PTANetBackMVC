namespace Retailer.Client;

public interface IApiService
{
    Task<IEnumerable<Retail>> Get(string path);
}
