using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using Stockit.Models;

namespace Stockit.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;
    private readonly IJSRuntime _jsRuntime;
    private User? _currentUser;
    private string? _token;

    public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _apiBaseUrl = $"{_httpClient.BaseAddress}api";
        _jsRuntime = jsRuntime;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        try
        {
            var request = new LoginRequest
            {
                Username = username,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}/auth/login", request);

            if (!response.IsSuccessStatusCode)
            {
                // Fallback to demo mode if API is unavailable
                return await DemoLoginAsync(username, password);
            }

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            
            if (loginResponse == null)
            {
                // Fallback to demo mode
                return await DemoLoginAsync(username, password);
            }

            _token = loginResponse.Token;
            _currentUser = loginResponse.User;

            // Store token in localStorage
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", _token);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "currentUser", System.Text.Json.JsonSerializer.Serialize(_currentUser));

            // Set authorization header for future requests
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login error: {ex.Message}");
            // Fallback to demo mode if API is unavailable
            return await DemoLoginAsync(username, password);
        }
    }

    private async Task<bool> DemoLoginAsync(string username, string password)
    {
        // Demo credentials for offline/demo mode
        User? demoUser = null;

        if (username == "admin" && password == "admin123")
        {
            demoUser = new User
            {
                UserID = 1,
                Username = "admin",
                FullName = "Admin User",
                Role = "Admin",
                Email = "admin@stockit.com",
                IsActive = true,
                CreatedDate = DateTime.Now,
                LastLoginDate = DateTime.Now
            };
            _token = "demo-admin-token";
        }
        else if (username == "supervisor" && password == "super123")
        {
            demoUser = new User
            {
                UserID = 2,
                Username = "supervisor",
                FullName = "Floor Supervisor",
                Role = "FloorSupervisor",
                Email = "supervisor@stockit.com",
                IsActive = true,
                CreatedDate = DateTime.Now,
                LastLoginDate = DateTime.Now
            };
            _token = "demo-supervisor-token";
        }

        if (demoUser != null)
        {
            _currentUser = demoUser;

            // Store in localStorage
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", _token);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "currentUser", System.Text.Json.JsonSerializer.Serialize(_currentUser));

            Console.WriteLine($"Demo login successful: {demoUser.FullName} ({demoUser.Role})");
            return true;
        }

        return false;
    }

    public async Task LogoutAsync()
    {
        _token = null;
        _currentUser = null;

        // Remove from localStorage
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "currentUser");

        // Clear authorization header
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<User?> GetCurrentUserAsync()
    {
        if (_currentUser != null)
            return _currentUser;

        // Try to load from localStorage
        try
        {
            var userJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "currentUser");
            
            if (!string.IsNullOrEmpty(userJson))
            {
                _currentUser = System.Text.Json.JsonSerializer.Deserialize<User>(userJson);
                
                // Also restore token
                _token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
                
                if (!string.IsNullOrEmpty(_token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading user: {ex.Message}");
        }

        return _currentUser;
    }

    public async Task<string?> GetTokenAsync()
    {
        if (_token != null)
            return _token;

        // Try to load from localStorage
        try
        {
            _token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        }
        catch { }

        return _token;
    }

    public bool IsAuthenticated()
    {
        return _currentUser != null || !string.IsNullOrEmpty(_token);
    }
}