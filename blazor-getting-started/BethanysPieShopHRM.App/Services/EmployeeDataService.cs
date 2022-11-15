using BethanysPieShopHRM.Shared;
using System.Text;
using System.Text.Json;

namespace BethanysPieShopHRM.App.Services;

public interface IEmployeeDataService
{
    Task<IEnumerable<Employee>?> GetEmployeesAsync();
    Task<Employee?> GetEmployeeAsync(int id);
    Task<Employee?> AddEmployeeAsync(Employee? employee);
    Task UpdateEmployeeAsync(Employee? employee);
    Task DeleteEmployeeAsync(int id);
}

public class EmployeeDataService : IEmployeeDataService
{
    private readonly HttpClient httpClient;

    public EmployeeDataService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<Employee>?> GetEmployeesAsync()
    {
        return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>(
            await httpClient.GetStreamAsync("api/employee"), CreateJsonSerializerOptions());
    }

    public async Task<Employee?> GetEmployeeAsync(int id)
    {
        return await JsonSerializer.DeserializeAsync<Employee>(
            await httpClient.GetStreamAsync($"api/employee/{id}"), CreateJsonSerializerOptions());
    }

    public async Task<Employee?> AddEmployeeAsync(Employee? employee)
    {
        var response = await httpClient.PostAsync("api/employee", CreateStringContent(employee));
        return response.IsSuccessStatusCode
            ? await JsonSerializer.DeserializeAsync<Employee>(await response.Content.ReadAsStreamAsync())
            : null;
    }

    public async Task UpdateEmployeeAsync(Employee? employee)
    {
        await httpClient.PutAsync("api/employee", CreateStringContent(employee));
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        await httpClient.DeleteAsync($"api/employee/{id}");
    }

    private static StringContent CreateStringContent(Employee? employee)
        => new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

    private static JsonSerializerOptions CreateJsonSerializerOptions()
        => new() { PropertyNameCaseInsensitive = true };
}
