namespace Retailer.Service;

public class RetailerService(IApiService apiService, IMediator mediator, IDistributedCache cache) : IRetailerService
{
    private const string retailers = "EXP01/Retailers";

    public async Task<Retail> GetRetailerById(string id)
    {
        var cachedData = await cache.GetStringAsync(id);

        if (cachedData is not null)
        {
            return JsonSerializer.Deserialize<Retail>(cachedData)!;
        }
        
        var response = await mediator.Send(new GetByIdQuery { Id = id });

        var serializedResponse = JsonSerializer.Serialize(response);

        await cache.SetStringAsync(id, serializedResponse, new DistributedCacheEntryOptions()
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(60),
            SlidingExpiration = TimeSpan.FromMinutes(60),
        });

        return response;
    }

    public async Task<IEnumerable<Retail>> Sync()
    {
        var response = await apiService.Get(retailers);

        _ = await mediator.Send(new SyncCommand { Retailers = response });

        return response ?? [];
    }
}
