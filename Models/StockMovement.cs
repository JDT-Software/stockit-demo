namespace Stockit.Models;

public class StockMovement
{
    public int MovementID { get; set; }
    public int ProductID { get; set; }
    public string MovementType { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public int PreviousQuantity { get; set; }
    public int NewQuantity { get; set; }
    public string? ReferenceType { get; set; }
    public int? ReferenceID { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? Notes { get; set; }
    
    // Navigation
    public Product? Product { get; set; }
}