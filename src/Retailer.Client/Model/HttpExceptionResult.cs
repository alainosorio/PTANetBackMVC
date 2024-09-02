namespace Retailer.Client.Model;

public record HttpExceptionResult
{
    public required virtual IDictionary Data { get; set; }
    public required virtual string Message { get; set; }
    public required virtual string Path { get; set; }
    public required virtual int StatusCode { get; set; }
}
