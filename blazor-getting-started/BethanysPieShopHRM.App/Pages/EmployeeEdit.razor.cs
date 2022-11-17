using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Pages;

public partial class EmployeeEdit
{
    protected string Message = string.Empty;
    protected string StatusClass = string.Empty;
    protected bool Saved = false;

    [Parameter]
    public int EmployeeId { get; set; }

    [Inject]
    public NavigationManager? NavigationManager { get; set; }

    [Inject]
    public IEmployeeDataService? EmployeeDataService { get; set; }

    [Inject]
    public ICountryDataService? CountryDataService { get; set; }

    [Inject]
    public IJobCategoryDataService? JobCategoryDataService { get; set; }

    public Employee? Employee { get; set; } = new();

    public List<Country>? Countries { get; set; } = new();

    public List<JobCategory>? JobCategories { get; set; } = new();

    protected async override Task OnInitializedAsync()
    {
        Saved = false;

        if (EmployeeDataService is not null)
        {
            Employee = EmployeeId <= 0
                ? CreateEmployeeDefault()
                : await EmployeeDataService.GetEmployeeAsync(EmployeeId);
        }

        if (CountryDataService is not null)
        {
            Countries = (await CountryDataService.GetCountriesAsync())?.ToList();
        }

        if (JobCategoryDataService is not null)
        {
            JobCategories = (await JobCategoryDataService.GetJobCategoriesAsync())?.ToList();
        }
    }

    protected async Task HandleValidSubmit()
    {
        if (EmployeeDataService is not null && Employee is not null)
        {
            if (Employee.EmployeeId <= 0)
            {
                var newEmployee = await EmployeeDataService.AddEmployeeAsync(Employee);
                StatusClass = newEmployee is null ? "alert-danger" : "alert-success";
                Message = newEmployee is null
                    ? "Adding new employee failed. Please try again"
                    : "Adding new employee completed successfully.";
                Saved = newEmployee is null ? false : true;
            }
            else
            {
                await EmployeeDataService.UpdateEmployeeAsync(Employee);
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
                Saved = true;
            }
        }
    }

    protected void HandleInvalidSubmit()
    {
        StatusClass = "alert-danger";
        Message = "There are some validation errors. Please try again.";
    }

    protected async Task DeleteEmployee()
    {
        if (EmployeeDataService is not null && Employee is not null)
        {
            await EmployeeDataService.DeleteEmployeeAsync(Employee.EmployeeId);
            StatusClass = "alert-success";
            Message = "Employee deleted successfully.";
            Saved = true;
        }
    }

    protected void NavigateToOverview()
        => NavigationManager?.NavigateTo("/employeeoverview");

    private static Employee CreateEmployeeDefault()
        => new()
        {
            CountryId = 1,
            JobCategoryId = 1,
            BirthDate = DateTime.Now,
            JoinedDate = DateTime.Now
        };
}
