using BethanysPieShopHRM.App.Components;
using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Pages;

public partial class EmployeeOverview
{
    public IEnumerable<Employee>? Employees { get; set; }

    [Inject]
    public IEmployeeDataService? EmployeeDataService { get; set; }

    protected AddEmployeeDialog? AddEmployeeDialog { get; set; }

    protected async override Task OnInitializedAsync()
    {
        if (EmployeeDataService is not null)
        {
            Employees = (await EmployeeDataService.GetEmployeesAsync())?.ToList();
        }
    }

    protected void QuickAddEmployee()
    {
        if (AddEmployeeDialog is not null)
        {
            AddEmployeeDialog.Show();
        }
    }

    protected async Task UpdateEmployees()
    {
        if (EmployeeDataService is not null)
        {
            Employees = (await EmployeeDataService.GetEmployeesAsync())?.ToList();
            StateHasChanged();
        }
    }
}