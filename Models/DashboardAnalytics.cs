namespace Stockit.Models;

public class StockDistribution
{
    public int HealthyStock { get; set; }
    public int LowStock { get; set; }
    public int CriticalStock { get; set; }
    public int OutOfStock { get; set; }
}

public class StockMovementTrend
{
    public string Date { get; set; } = string.Empty;
    public int Purchases { get; set; }
    public int Adjustments { get; set; }
    public int Damaged { get; set; }
    public int Returns { get; set; }
}

public class CategoryStockSummary
{
    public string CategoryName { get; set; } = string.Empty;
    public int TotalUnits { get; set; }
    public int ProductCount { get; set; }
}

public class LowStockProduct
{
    public string ProductCode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int CurrentStock { get; set; }
    public int ReorderPoint { get; set; }
    public int Gap { get; set; }
}