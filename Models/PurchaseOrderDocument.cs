namespace Stockit.Models;

public class PurchaseOrderDocument
{
    public int DocumentID { get; set; }
    public int PurchaseOrderID { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public DateTime UploadedAt { get; set; }
    public string? UploadedBy { get; set; }
}
