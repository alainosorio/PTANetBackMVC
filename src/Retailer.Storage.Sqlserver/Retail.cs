namespace Retailer.Storage.Sqlserver;

public class Retail
{
    [Key]
    public required string Id { get; set; }
    public required string Code { get; set; }
    public required string CodingScheme { get; set; }
    public required string Country { get; set; }
    public required string Name { get; set; }
}
