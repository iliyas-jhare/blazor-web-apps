using BethanysPieShopHRM.App;
using BethanysPieShopHRM.App.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUri = new Uri("https://localhost:44340/");
builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = apiBaseUri);
builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(client => client.BaseAddress = apiBaseUri);
builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(client => client.BaseAddress = apiBaseUri);

await builder.Build().RunAsync();
