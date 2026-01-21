using Stockit.Models;

namespace Stockit.Services;

public interface IAuthorizationService
{
    Task<bool> CanAccessDashboardAsync();
    Task<bool> CanAccessStockEntryAsync();
    Task<bool> CanAccessProductsAsync();
    Task<bool> CanAccessPurchaseOrdersAsync();
    Task<bool> CanAccessSuppliersAsync();
    Task<bool> CanAccessAuditTrailAsync();
    Task<bool> IsAdminAsync();
}

public class AuthorizationService : IAuthorizationService
{
    private readonly IAuthService _authService;

    public AuthorizationService(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<bool> CanAccessDashboardAsync()
    {
        return await IsAdminAsync();
    }

    public async Task<bool> CanAccessStockEntryAsync()
    {
        // Both Admin and FloorSupervisor can access
        var user = await _authService.GetCurrentUserAsync();
        return user != null;
    }

    public async Task<bool> CanAccessProductsAsync()
    {
        // Both Admin and FloorSupervisor can access
        var user = await _authService.GetCurrentUserAsync();
        return user != null;
    }

    public async Task<bool> CanAccessPurchaseOrdersAsync()
    {
        return await IsAdminAsync();
    }

    public async Task<bool> CanAccessSuppliersAsync()
    {
        return await IsAdminAsync();
    }

    public async Task<bool> CanAccessAuditTrailAsync()
    {
        return await IsAdminAsync();
    }

    public async Task<bool> IsAdminAsync()
    {
        var user = await _authService.GetCurrentUserAsync();
        return user?.Role == "Admin" || user?.Role == "Administrator";
    }
}