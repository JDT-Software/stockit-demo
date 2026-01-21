using System.Net.Http.Json;
using Stockit.Models;

namespace Stockit.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiBaseUrl = $"{_httpClient.BaseAddress}api";
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        try
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>("api/products");
            return products ?? new List<Product>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching products: {ex.Message}");
            return new List<Product>();
        }
    }

    public async Task<List<Product>> GetProductsByCategoryAsync(int categoryId)
    {
        try
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>($"api/products/category/{categoryId}");
            return products ?? new List<Product>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching products by category: {ex.Message}");
            return new List<Product>();
        }
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        try
        {
            var categories = await _httpClient.GetFromJsonAsync<List<Category>>("api/categories");
            return categories ?? new List<Category>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching categories: {ex.Message}");
            return new List<Category>();
        }
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/products/{productId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching product: {ex.Message}");
            return null;
        }
    }

    public async Task<StockLevel?> GetStockLevelAsync(int productId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<StockLevel>($"api/stock/{productId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching stock level: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> UpdateStockLevelAsync(int productId, int newQuantity, int userId)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"api/stock/{productId}", new
            {
                ProductId = productId,
                Quantity = newQuantity,
                UserId = userId
            });
            
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating stock level: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateStockLevelAsync(
        int productId,
        int newQuantity,
        string movementType,
        string notes
    )
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"{_apiBaseUrl}/products/{productId}/stock",
                new
                {
                    NewQuantity = newQuantity,
                    MovementType = movementType,
                    Notes = notes
                }
            );
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating stock level: {ex.Message}");
            return false;
        }
    }

    public async Task<Supplier?> GetPrimarySupplierForProductAsync(int productId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Supplier>($"api/products/{productId}/supplier");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching supplier for product: {ex.Message}");
            return null;
        }
    }

    public async Task<List<StockMovement>> GetAllStockMovementsAsync()
    {
        try
        {
            var movements = await _httpClient.GetFromJsonAsync<List<StockMovement>>("api/stockmovements");
            return movements ?? new List<StockMovement>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching stock movements: {ex.Message}");
            return new List<StockMovement>();
        }
    }

    public async Task<List<StockMovement>> GetStockMovementsByProductAsync(int productId)
    {
        try
        {
            var movements = await _httpClient.GetFromJsonAsync<List<StockMovement>>($"api/stockmovements/product/{productId}");
            return movements ?? new List<StockMovement>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching stock movements: {ex.Message}");
            return new List<StockMovement>();
        }
    }

    public async Task<StockDistribution> GetStockDistributionAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<StockDistribution>($"{_apiBaseUrl}/products/analytics/distribution") 
                ?? new StockDistribution();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching distribution: {ex.Message}");
            return new StockDistribution();
        }
    }

    public async Task<List<StockMovementTrend>> GetStockMovementTrendAsync(int days = 30)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<StockMovementTrend>>($"{_apiBaseUrl}/products/analytics/trend?days={days}") 
                ?? new List<StockMovementTrend>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching trend: {ex.Message}");
            return new List<StockMovementTrend>();
        }
    }

    public async Task<List<CategoryStockSummary>> GetCategoryStockSummaryAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<CategoryStockSummary>>($"{_apiBaseUrl}/products/analytics/categories") 
                ?? new List<CategoryStockSummary>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching categories: {ex.Message}");
            return new List<CategoryStockSummary>();
        }
    }
}