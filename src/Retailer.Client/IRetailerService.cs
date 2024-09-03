namespace Retailer.Client;

public interface IRetailerService
{
    [Post("/api/retailers/sync")]
    Task<IEnumerable<Retail>> Sync();
    
    [Get("/api/retailers/{id}")]
    Task<Retail> GetRetailerById(string id);
}
