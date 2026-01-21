using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Stockit;
using Stockit.Services;  
using MudBlazor.Services;
using Fluxor;
using Blazored.LocalStorage;
using Fluxor.Blazor.Web.ReduxDevTools;
using ApexCharts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://stockit-api-jacques.azurewebsites.net";

builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri(apiBaseUrl) 
});

// Services with optimized lifetimes
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddApexCharts();

// State Management (Fluxor)
builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(typeof(Program).Assembly);
    options.UseReduxDevTools(); // Browser DevTools integration
    options.UseRouting();
});

// Local Storage
builder.Services.AddBlazoredLocalStorage();

// MudBlazor UI Components
builder.Services.AddMudServices();

await builder.Build().RunAsync();