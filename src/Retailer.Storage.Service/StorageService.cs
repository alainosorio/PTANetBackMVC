namespace Retailer.Storage.Service;

public class StorageService(RetailerDbContext dbContext) : IStorageService
{
    public async Task Sync(IEnumerable<Client.Model.Retail> retails)
    {
        // Get the existing items in the database that match the provided items
        var itemNames = retails.Select(i => i.Name).ToList();

        var existingItems = await dbContext.Retails
            .Where(e => itemNames.Contains(e.Name))
            .Select(e => e.Name)
            .ToListAsync();

        // Filter out items that already exist in the database
        var newItems = retails.Where(i => !existingItems.Contains(i.Name)).ToList();

        // Create new entities for the items that don't exist
        var entitiesToInsert = newItems.Select(item => new Retail 
        { 
            Code = item.Code,
            CodingScheme = item.CodingScheme,
            Country = item.Country,
            Id = $"{item.Country}-{item.Code}",
            Name = item.Name,
        }).ToList();

        if (entitiesToInsert.Count != 0)
        {
            // Bulk insert the new items
            await dbContext.Retails.AddRangeAsync(entitiesToInsert);
            await dbContext.SaveChangesAsync();
        }
    }
}
