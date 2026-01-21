namespace Stockit.Models;

public class PurchaseOrder
{
    public int PurchaseOrderID { get; set; }
    public string PONumber { get; set; } = string.Empty; // Changed
    public int SupplierID { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime? ExpectedDeliveryDate { get; set; }
    public DateTime? ActualDeliveryDate { get; set; }
    public string Status { get; set; } = "Draft";
    public decimal TotalAmount { get; set; }
    public string? Notes { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime SentDate { get; set; } // Changed
    
    public Supplier? Supplier { get; set; }
    public List<PurchaseOrderItem> Items { get; set; } = new();
}

public class PurchaseOrderItem
{
    public int POItemID { get; set; }  // Changed
    public int PurchaseOrderID { get; set; }
    public int ProductID { get; set; }
    public int OrderedQuantity { get; set; }  // Changed
    public int ReceivedQuantity { get; set; }  // Changed
    public decimal UnitCost { get; set; }  // Changed
    public decimal Subtotal => OrderedQuantity * UnitCost;  // Updated calculation
    
    public PurchaseOrder? PurchaseOrder { get; set; }
    public Product? Product { get; set; }
}
