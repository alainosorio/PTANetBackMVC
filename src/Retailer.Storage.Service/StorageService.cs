namespace Retailer.Storage.Service;

public class StorageService(RetailerDbContext dbContext, IMapper mapper) : IStorageService
{
    public async Task<Client.Model.Retail> GetRetailerById(string id)
    {
        var response = await dbContext.Retails.FindAsync(id);

        return mapper.Map<Client.Model.Retail>(response);
    }

    public async Task Sync(IEnumerable<Client.Model.Retail> items)
    {
        // Extract unique pairs of Country and Code from the input items
        var itemKeys = items.Select(i => new { i.Country, i.Code }).Distinct().ToList();

        // Perform a bulk existence check for items that have the same Country and Code
        var existingItems = await dbContext.Retails.AsNoTracking().ToListAsync();
        
        var itemsToCompare = existingItems.Where(i => itemKeys.Contains(new { i.Country, i.Code }));

        // Filter out the items that already exist in the database
        var newItems = items
            .Where(i => !itemsToCompare.Any(e => e.Name == i.Name && e.Code == i.Code))
            .Select(i => new Retail
            {
                Code = i.Code,
                CodingScheme = i.CodingScheme,
                Country = i.Country,
                Id = $"{i.Country}-{i.Code}",
                Name = i.Name,
            })
            .ToList();

        // Bulk insert the new items
        if (newItems.Count != 0)
        {
            await dbContext.Retails.AddRangeAsync(newItems);
            await dbContext.SaveChangesAsync();
        }
    }
}
