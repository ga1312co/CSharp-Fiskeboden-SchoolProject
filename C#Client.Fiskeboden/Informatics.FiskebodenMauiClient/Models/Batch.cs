namespace Informatics.FiskebodenMauiClient.Models;
using System.Text.Json.Serialization;


public record class Batch
{
    [JsonPropertyName("batchNo")]
    public string BatchNo { get; set; } = null!;

    [JsonPropertyName("batchWeek")]
    public int? BatchWeek { get; set; }

    [JsonPropertyName("batchQuantity")]
    public decimal? BatchQuantity { get; set; }

    [JsonPropertyName("productionDate")]
    public DateOnly ProductionDate { get; set; }

    [JsonPropertyName("batchShelfLife")]
    public int BatchShelfLife { get; set; }

    [JsonPropertyName("batchPrice")]
    public decimal BatchPrice { get; set; }

    [JsonPropertyName("batchOrigin")]
    public string BatchOrigin { get; set; } = null!;

    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = null!;

    [JsonPropertyName("supplierName")]
    public string SupplierName { get; set; } = null!;
}
