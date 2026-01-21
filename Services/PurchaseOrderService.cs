using System.Net.Http.Json;
using Stockit.Models;

namespace Stockit.Services;

public class PurchaseOrderService : IPurchaseOrderService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;

    public PurchaseOrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiBaseUrl = $"{_httpClient.BaseAddress}api";
    }

    public async Task<List<PurchaseOrder>> GetAllPurchaseOrdersAsync()
    {
        try
        {
            var orders = await _httpClient.GetFromJsonAsync<List<PurchaseOrder>>("api/purchaseorders");
            return orders ?? new List<PurchaseOrder>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching purchase orders: {ex.Message}");
            return new List<PurchaseOrder>();
        }
    }

    public async Task<List<PurchaseOrder>> GetPurchaseOrdersBySupplierAsync(int supplierId)
    {
        try
        {
            var orders = await _httpClient.GetFromJsonAsync<List<PurchaseOrder>>($"api/purchaseorders/supplier/{supplierId}");
            return orders ?? new List<PurchaseOrder>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching purchase orders: {ex.Message}");
            return new List<PurchaseOrder>();
        }
    }

    public async Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<PurchaseOrder>($"api/purchaseorders/{id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching purchase order: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/purchaseorders", purchaseOrder);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating purchase order: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/purchaseorders/{purchaseOrder.PurchaseOrderID}", purchaseOrder);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating purchase order: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> ReceivePurchaseOrderAsync(int id, List<PurchaseOrderItem> receivedItems)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"api/purchaseorders/{id}/receive", receivedItems);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error receiving purchase order: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> CancelPurchaseOrderAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/purchaseorders/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error cancelling purchase order: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> SendToSupplierAsync(int purchaseOrderId)
    {
        try
        {
            var response = await _httpClient.PostAsync($"api/purchaseorders/{purchaseOrderId}/send-to-supplier", null);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending PO to supplier: {ex.Message}");
            return false;
        }
    }

    public async Task<byte[]?> DownloadPdfAsync(int purchaseOrderId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/purchaseorders/{purchaseOrderId}/download-pdf");
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }
            
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error downloading PDF: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> UpdateStatusAsync(int purchaseOrderId, string status)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"api/purchaseorders/{purchaseOrderId}/status", 
                new { Status = status }
            );
            
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating PO status: {ex.Message}");
            return false;
        }
    }

    public async Task<List<PurchaseOrderDocument>> GetDocumentsAsync(int purchaseOrderId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/documents/purchase-order/{purchaseOrderId}");
            
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<PurchaseOrderDocument>>() ?? new List<PurchaseOrderDocument>();
            }
            
            return new List<PurchaseOrderDocument>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching documents: {ex.Message}");
            return new List<PurchaseOrderDocument>();
        }
    }
}