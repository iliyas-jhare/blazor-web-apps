using System.Text.Json;
using BethanysPieShopHRM.Shared;

namespace BethanysPieShopHRM.App.Services;

public interface ICountryDataService
{
    Task<IEnumerable<Country>?> GetCountriesAsync();
    Task<Country?> GetCountryAsync(int id);
}

public class CountryDataService : ICountryDataService
{
    private readonly HttpClient httpClient;

    public CountryDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<Country>?> GetCountriesAsync()
    {
        return await JsonSerializer.DeserializeAsync<IEnumerable<Country>>(
            await httpClient.GetStreamAsync("api/country"), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
    }

    public async Task<Country?> GetCountryAsync(int id)
    {
        return await JsonSerializer.DeserializeAsync<Country>(
            await httpClient.GetStreamAsync($"api/country/{id}"), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
    }
}