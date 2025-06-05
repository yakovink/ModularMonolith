using System;
using System.Text.Json;


namespace Shared.Communicate;

public class HttpController : HttpClient
{
    private string url { get; set; }
    private int port { get; set; }
    public HttpController(string url, int port) : base()
    {
        this.url = url;
        this.port = port;
    }

    public static string JsonSerialize(object obj)
    {
        return System.Text.Json.JsonSerializer.Serialize(obj);
    }

    public static StringContent JsonSerializeContent(object obj)
    {
        var json = JsonSerialize(obj);
        return new StringContent(json, System.Text.Encoding.UTF8, "application/json");
    }

    public static JsonDocument JsonDeserialize(string json)
    {
        return JsonDocument.Parse(json);
    }

    public async Task<JsonDocument> Post(string endpoint, object content, CancellationToken cancellationToken = default)
    {
        string url = $"{this.url}:{this.port}/{endpoint}";
        StringContent jsonContent = JsonSerializeContent(content);
        HttpResponseMessage response = await PostAsync(url, jsonContent, cancellationToken);
        string responseString = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonDeserialize(responseString);
    }

    public async Task<JsonDocument> Get(string endpoint, CancellationToken cancellationToken = default)
    {
        string url = $"{this.url}:{this.port}/{endpoint}";
        HttpResponseMessage response = await GetAsync(url, cancellationToken);
        Console.WriteLine(response.Content);
        string responseString = await response.Content.ReadAsStringAsync(cancellationToken);
        Console.WriteLine(responseString);
        return JsonDeserialize(responseString);
    }

    public async Task<JsonDocument> Put(string endpoint, object content, CancellationToken cancellationToken = default)
    {
        string url = $"{this.url}:{this.port}/{endpoint}";
        StringContent jsonContent = JsonSerializeContent(content);
        HttpResponseMessage response = await PutAsync(url, jsonContent, cancellationToken);
        string responseString = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonDeserialize(responseString);
    }
    
    public async Task<JsonDocument> Delete(string endpoint, CancellationToken cancellationToken = default)
    {
        string url = $"{this.url}:{this.port}/{endpoint}";
        HttpResponseMessage response = await DeleteAsync(url, cancellationToken);
        string responseString = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonDeserialize(responseString);
    }   

}
