using Stockit.Models;

namespace Stockit.Services;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<List<Product>> GetProductsByCategoryAsync(int categoryId);
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<StockLevel?> GetStockLevelAsync(int productId);
    Task<bool> UpdateStockLevelAsync(int productId, int newQuantity, int userId);
    Task<bool> UpdateStockLevelAsync(
        int productId,
        int newQuantity,
        string movementType,
        string notes
    );
    Task<Supplier?> GetPrimarySupplierForProductAsync(int productId);

    // Stock Movement methods
    Task<List<StockMovement>> GetAllStockMovementsAsync();
    Task<List<StockMovement>> GetStockMovementsByProductAsync(int productId);

    // Analytics methods
    Task<StockDistribution> GetStockDistributionAsync();
    Task<List<StockMovementTrend>> GetStockMovementTrendAsync(int days = 30);
    Task<List<CategoryStockSummary>> GetCategoryStockSummaryAsync();
}