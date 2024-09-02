namespace Retailer.Service;

public class ApiService(HttpClient httpClient) : IApiService
{
    public async Task<IEnumerable<Retail>> Get(string path)
    {
        var data = await httpClient.GetAsync(path);

        data.EnsureSuccessStatusCode();

        var result = await data.Content.ReadFromJsonAsync<IEnumerable<Retail>>();

        return result ?? [];
    }
}
