using Stockit.Models;

namespace Stockit.Services;

public interface IPurchaseOrderService
{
    Task<List<PurchaseOrder>> GetAllPurchaseOrdersAsync();
    Task<List<PurchaseOrder>> GetPurchaseOrdersBySupplierAsync(int supplierId);
    Task<PurchaseOrder?> GetPurchaseOrderByIdAsync(int id);
    Task<bool> CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder);
    Task<bool> UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder);
    Task<bool> ReceivePurchaseOrderAsync(int id, List<PurchaseOrderItem> receivedItems);
    Task<bool> CancelPurchaseOrderAsync(int id);
    Task<bool> SendToSupplierAsync(int purchaseOrderId);
    Task<byte[]?> DownloadPdfAsync(int purchaseOrderId);
    Task<bool> UpdateStatusAsync(int purchaseOrderId, string status);
    Task<List<PurchaseOrderDocument>> GetDocumentsAsync(int purchaseOrderId);
}