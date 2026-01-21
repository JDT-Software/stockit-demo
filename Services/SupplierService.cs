using System.Net.Http.Json;
using Stockit.Models;

namespace Stockit.Services;

public class SupplierService : ISupplierService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;

    public SupplierService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiBaseUrl = $"{_httpClient.BaseAddress}api";
    }

    public async Task<List<Supplier>> GetAllSuppliersAsync()
    {
        try
        {
            var suppliers = await _httpClient.GetFromJsonAsync<List<Supplier>>("api/suppliers");
            return suppliers ?? new List<Supplier>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching suppliers: {ex.Message}");
            return new List<Supplier>();
        }
    }

    public async Task<Supplier?> GetSupplierByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Supplier>($"api/suppliers/{id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching supplier: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> CreateSupplierAsync(Supplier supplier)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/suppliers", supplier);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating supplier: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdateSupplierAsync(Supplier supplier)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/suppliers/{supplier.SupplierID}", supplier);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating supplier: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteSupplierAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/suppliers/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting supplier: {ex.Message}");
            return false;
        }
    }
}