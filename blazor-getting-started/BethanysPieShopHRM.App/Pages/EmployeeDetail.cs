
using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Pages;

public partial class EmployeeDetail
{
    [Parameter]
    public int EmployeeId { get; set; }

    [Inject]
    public IEmployeeDataService? EmployeeDataService { get; set; }

    public Employee? Employee { get; set; } = new();

    protected async override Task OnInitializedAsync()
    {
        if (EmployeeDataService is not null)
        {
            Employee = await EmployeeDataService.GetEmployeeAsync(EmployeeId);
        }
    }
}