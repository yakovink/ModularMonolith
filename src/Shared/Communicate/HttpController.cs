


namespace Shared.Communicate;

public interface IHttpController
{
    public Task<JsonElement> Get(string url,CancellationToken cancellationToken);
    public Task<JsonElement> Post(string url, object content, CancellationToken cancellationToken);
    public Task<JsonElement> Put(string url, object content, CancellationToken cancellationToken);
    public Task<JsonElement> Delete(string url, CancellationToken cancellationToken);
}
public class HttpController : HttpClient, IHttpController
{
    private string url { get; set; }
    private int port { get; set; }
    public HttpController(string url, int port) : base()
    {
        this.url = url;
        this.port = port;
    }

    private static string JsonSerialize(object obj)
    {
        return JsonSerializer.Serialize(obj);
    }

    private static StringContent JsonSerializeContent(object obj)
    {
        var json = JsonSerialize(obj);
        return new StringContent(json, System.Text.Encoding.UTF8, "application/json");
    }

    private async static Task<JsonElement> JsonDeserialize(HttpResponseMessage response,CancellationToken cancellationToken)
    {
        string responseString = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonDocument.Parse(responseString).RootElement.GetProperty("output");
    }

    public async Task<JsonElement> Post(string endpoint, object content, CancellationToken cancellationToken = default)
    {
        string url = buildUrl(endpoint);
        Console.WriteLine(url);
        StringContent jsonContent = JsonSerializeContent(content);
        HttpResponseMessage response = await PostAsync(url, jsonContent, cancellationToken);
        return await JsonDeserialize(response,cancellationToken);
    }

    public async Task<JsonElement> Get(string endpoint, CancellationToken cancellationToken = default)
    {
        string url = buildUrl(endpoint);
        
        HttpResponseMessage response = await GetAsync(url, cancellationToken);
        return await JsonDeserialize(response, cancellationToken);
    }

    public async Task<JsonElement> Put(string endpoint, object content, CancellationToken cancellationToken = default)
    {
        string url = buildUrl(endpoint);
        StringContent jsonContent = JsonSerializeContent(content);
        HttpResponseMessage response = await PutAsync(url, jsonContent, cancellationToken);
        return await JsonDeserialize(response, cancellationToken);
    }
    
    public async Task<JsonElement> Delete(string endpoint, CancellationToken cancellationToken = default)
    {
        string url = buildUrl(endpoint);
        HttpResponseMessage response = await DeleteAsync(url, cancellationToken);
        return await JsonDeserialize(response, cancellationToken);
    }

    private string buildUrl(string endpoint)
    {
        return $"{this.url}:{this.port}/{endpoint}";
    }



}
