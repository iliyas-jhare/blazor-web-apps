
using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.ComponentsLibrary.Map;
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

    public List<Marker>? MapMarkers { get; set; } = new();

    protected async override Task OnInitializedAsync()
    {
        if (EmployeeDataService is not null)
        {
            Employee = await EmployeeDataService.GetEmployeeAsync(EmployeeId);
            MapMarkers = new List<Marker>()
            {
                new Marker() { Description = $"{Employee.FirstName} {Employee.LastName}", ShowPopup = false, X = Employee.Longitude, Y = Employee.Latitude }
            };
        }
    }
}