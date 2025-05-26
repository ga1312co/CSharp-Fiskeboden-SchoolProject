namespace Informatics.FiskebodenWebAPI.DTOs;

public class BatchDto
{    
    // Har exkluderat IDs (surrogate keys) från DTOs då Backend ska vara readonly
    public string BatchNo { get; set; } = null!;

    public int? BatchWeek { get; set; }

    public decimal? BatchQuantity { get; set; }

    public DateOnly ProductionDate { get; set; }

    public int BatchShelfLife { get; set; }

    public decimal BatchPrice { get; set; }

    public string BatchOrigin { get; set; } = null!;

// Inkluderar ProductName och SupplierName som strängar istället för att inkludera hela Product och Supplier objekt
    public string ProductName { get; set; } = null!;

    public string SupplierName { get; set; } = null!;

}
