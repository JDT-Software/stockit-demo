using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using MudBlazor.Services;
using Stockit;
using Stockit.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add LocalStorage
builder.Services.AddBlazoredLocalStorage();

// Add MudBlazor
builder.Services.AddMudServices();

// Add AuthService
builder.Services.AddScoped<IAuthService, AuthService>();

// Configure HttpClient with JWT token
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "https://stockit-api-jacques.azurewebsites.net";

builder.Services.AddScoped(sp =>
{
    var localStorage = sp.GetRequiredService<ILocalStorageService>();
    var httpClient = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };
    
    // Try to get token from localStorage
    try
    {
        var token = localStorage.GetItemAsStringAsync("authToken").GetAwaiter().GetResult();
        if (!string.IsNullOrEmpty(token))
        {
            // Remove quotes if present
            token = token.Trim('"');
            httpClient.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
    catch
    {
        // No token yet, that's ok
    }
    
    return httpClient;
});

await builder.Build().RunAsync();