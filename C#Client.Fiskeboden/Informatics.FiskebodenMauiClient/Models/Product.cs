using System.Text.Json.Serialization;

namespace Informatics.FiskebodenMauiClient.Models;

public record class Product
{
    [JsonPropertyName("productNo")]
    public string ProductNo { get; set; }
    
    [JsonPropertyName("productName")]
    public string ProductName { get; set; }

    [JsonPropertyName("isMeasuredInUnits")]
    public bool IsMeasuredInUnits { get; set; }

    [JsonPropertyName("categoryName")]
    public string CategoryName { get; set; }

    [JsonPropertyName("batchNumbers")]
    public List<string> BatchNumbers { get; set; }
}
