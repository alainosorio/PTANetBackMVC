namespace Retailer.Service;

public class RetailerService(IApiService apiService, IMediator mediator) : IRetailerService
{
    private const string retailers = "EXP01/Retailers";

    public Task<IEnumerable<Retail>> CreateRetailer(IEnumerable<Retail> retailers)
    {
        throw new NotImplementedException();
    }

    public Task<Retail> GetRetailerById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Retail>> Sync()
    {
        var response = await apiService.Get(retailers);

        _ = await mediator.Send(new SyncCommand { Retailers = response });

        return response ?? [];
    }
}
