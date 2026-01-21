using Stockit.Models;

namespace Stockit.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(string username, string password);
    Task LogoutAsync();
    Task<User?> GetCurrentUserAsync();
    Task<string?> GetTokenAsync();
    bool IsAuthenticated();
}