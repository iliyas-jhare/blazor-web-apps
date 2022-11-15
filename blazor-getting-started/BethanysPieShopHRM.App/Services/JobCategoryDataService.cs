using System.Text.Json;
using BethanysPieShopHRM.Shared;

namespace BethanysPieShopHRM.App.Services;

public interface IJobCategoryDataService
{
    Task<IEnumerable<JobCategory>?> GetJobCategoriesAsync();
    Task<JobCategory?> GetJobCategoryAsync(int id);
}

public class JobCategoryDataService : IJobCategoryDataService
{
    private readonly HttpClient httpClient;

    public JobCategoryDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<JobCategory>?> GetJobCategoriesAsync()
    {
        return await JsonSerializer.DeserializeAsync<IEnumerable<JobCategory>>(
            await httpClient.GetStreamAsync("api/jobcategory"), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
    }

    public async Task<JobCategory?> GetJobCategoryAsync(int id)
    {
        return await JsonSerializer.DeserializeAsync<JobCategory>(
            await httpClient.GetStreamAsync($"api/jobcategory/{id}"), new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );
    }
}