using System;
using System.Text.Json;

namespace Account.Users.Models;

public record CountriesList
{
    public string url { get; init; } = "restcountries.com";
    public int port { get; init; } = 443;
    public string endpoint { get; init; } = "/v3.1/all";
}

public record CitiesByCountry
{
    public string url { get; init; } = "countriesnow.space";
    public int port { get; init; } = 443;
    public string endpoint { get; init; } = "/api/v0.1/countries/cities";
}

public class Address : Entity<Guid>
{
    public string Street { get; private set; } = default!;
    public string City { get; private set; } = default!;
    public string State { get; private set; } = default!;
    public string ZipCode { get; private set; } = default!;
    public string Country { get; private set; } = default!;


    public const string CitiesByCountryUrl = "https://countriesnow.space/api/v0.1/countries/cities";

    public static Address Create(Guid id, string street, string city, string state, string zipCode, string country)
    {
        ArgumentException.ThrowIfNullOrEmpty(street);
        ArgumentException.ThrowIfNullOrEmpty(city);
        ArgumentException.ThrowIfNullOrEmpty(state);
        ArgumentException.ThrowIfNullOrEmpty(zipCode);
        ArgumentException.ThrowIfNullOrEmpty(country);

        return new Address
        {
            Id = id,
            Street = street,
            City = city,
            State = state,
            ZipCode = zipCode,
            Country = country
        };

    }

    public static async Task<List<string>> GetCountriesAsync()
    {
        var list = new CountriesList();
        HttpController? controller = new HttpController(list.url, list.port);
        if (controller == null)
            throw new InternalServerException("HttpController could not be created.");
        JsonDocument? responseDoc = await controller.Get(list.endpoint);
        
        return responseDoc.RootElement.EnumerateArray()
            .Where(element => element.TryGetProperty("name", out JsonElement nameProp) &&
                              nameProp.TryGetProperty("common", out JsonElement commonProp))
            .Select(element => element.GetProperty("name").GetProperty("common").GetString()!)
            .ToList();
    }

    public static async Task<List<string>> GetCitiesByCountryAsync(string country)
    {
        if (string.IsNullOrWhiteSpace(country))
            throw new ArgumentException("Country must not be empty.", nameof(country));
        var list = new CitiesByCountry();
        HttpController? controller = new HttpController(list.url, list.port);
        if (controller == null)
            throw new InternalServerException("HttpController could not be created.");
        JsonDocument? doc = await controller.Post(list.endpoint, new { country });
        List<string> cities = new List<string>();
        if (doc.RootElement.TryGetProperty("data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
        {
            foreach (JsonElement cityElement in dataElement.EnumerateArray())
            {
                cities.Add(cityElement.GetString()!);
            }
        }
        return cities;
    }
    
    public override string ToString()
    {
        return $"{Street}, {City}, {State}, {ZipCode}, {Country}";
    }
}
