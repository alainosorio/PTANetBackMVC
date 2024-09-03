namespace Retailer.Service.Command;

public record SyncCommand : IRequest<Unit> 
{ 
    public required IEnumerable<Retail> Retailers { get; init; }
}

public class SyncCommandHandler(IStorageService storageService) : IRequestHandler<SyncCommand, Unit>
{
    public async Task<Unit> Handle(SyncCommand request, CancellationToken cancellationToken)
    {
        await storageService.Sync(request.Retailers);
        
        return Unit.Value;
    }
}
