namespace Retailer.Storage.Sqlserver;

public class RetailerDbContext(DbContextOptions<RetailerDbContext> options) : DbContext(options)
{
    public DbSet<Retail> Retails { get; set; }
}
