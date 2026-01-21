namespace Stockit.Models;

public class Product
{
    public int ProductID { get; set; }
    public int CategoryID { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string UnitOfMeasure { get; set; } = string.Empty;
    public int ReorderPoint { get; set; }
    public int ReorderQuantity { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; }
    
    // Navigation properties
    public Category? Category { get; set; }
    public StockLevel? StockLevel { get; set; }
}

public class Category
{
    public int CategoryID { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    
    // Navigation
    public List<Product> Products { get; set; } = new();
}

public class StockLevel
{
    public int StockLevelID { get; set; }
    public int ProductID { get; set; }
    public int CurrentQuantity { get; set; }
    public DateTime? LastCountDate { get; set; }
    public int? LastCountedBy { get; set; }
    public DateTime LastUpdated { get; set; }
    
    // Navigation
    public Product? Product { get; set; }
}