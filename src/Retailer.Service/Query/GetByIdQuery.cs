namespace Retailer.Service.Query;

public record GetByIdQuery : IRequest<Retail>
{
    public required string Id { get; init; }
}

public class GetByIdQueryHandler(IStorageService storageService) : IRequestHandler<GetByIdQuery, Retail>
{
    public Task<Retail> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        return storageService.GetRetailerById(request.Id);
    }
}