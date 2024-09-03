namespace Retailer.Storage.Sqlserver;

public class RetailerDbContext(DbContextOptions<RetailerDbContext> options) : DbContext(options)
{
    public virtual DbSet<Retail> Retails { get; set; }
}
