namespace Retailer.Service;

public class RetailerService(IApiService apiService, IMediator mediator) : IRetailerService
{
    private const string retailers = "EXP01/Retailers";

    public async Task<Retail> GetRetailerById(string id)
    {
        return await mediator.Send(new GetByIdQuery { Id = id });
    }

    public async Task<IEnumerable<Retail>> Sync()
    {
        var response = await apiService.Get(retailers);

        _ = await mediator.Send(new SyncCommand { Retailers = response });

        return response ?? [];
    }
}
