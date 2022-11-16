using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Components;

public partial class AddEmployeeDialog
{
    public Employee? Employee { get; set; } = CreateEmployeeDefault();

    [Inject]
    public IEmployeeDataService? EmployeeDataService { get; set; }

    public bool ShowDialog { get; set; } = false;

    [Parameter]
    public EventCallback<bool> ClosedCallback { get; set; }

    public void Show()
    {
        ResetDialog();
        ShowDialog = true;
        StateHasChanged();
    }

    public void Close()
    {
        ShowDialog = false;
        StateHasChanged();
    }

    protected async Task HandleValidSubmit()
    {
        if (EmployeeDataService is not null && Employee is not null)
        {
            var newEmployee = await EmployeeDataService.AddEmployeeAsync(Employee);
            if (newEmployee is not null)
            {
                await ClosedCallback.InvokeAsync(true);
                Close();
            }
        }
    }

    private void ResetDialog()
        => Employee = CreateEmployeeDefault();

    private static Employee CreateEmployeeDefault()
        => new()
        {
            CountryId = 1,
            JobCategoryId = 1,
            BirthDate = DateTime.Now,
            JoinedDate = DateTime.Now
        };
}

