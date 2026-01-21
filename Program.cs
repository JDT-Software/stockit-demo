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

// HTTP Client for API calls
// Use the host's base address to determine the API URL
var baseAddress = builder.HostEnvironment.BaseAddress;
var apiUrl = baseAddress.Contains("localhost") 
    ? "http://localhost:5041" 
    : "http://192.168.0.186:5041"; // Your PC's IP address

builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri(apiUrl)
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