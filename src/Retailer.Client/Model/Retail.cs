namespace Retailer.Client.Model;

public record Retail
{
    [JsonPropertyName("reName")]
    public required string Name { get; set; }

    [JsonPropertyName("country")]
    public required string Country { get; set; }

    [JsonPropertyName("codingScheme")]
    public required string CodingScheme { get; set; }

    [JsonPropertyName("reCode")]
    public required string Code { get; set; }
}
