namespace Informatics.FiskebodenMauiClient.Models;
using System.Text.Json.Serialization;

public record class PickupPoint
{
    [JsonPropertyName("PickupPointNo")]
    public string PickupPointNo { get; set; } = null!;

    [JsonPropertyName("PickupPointAddress")]
    public string PickupPointAddress { get; set; } = null!;

    [JsonPropertyName("PickupPointName")]
    public string PickupPointName { get; set; } = null!;

    [JsonPropertyName("SupplierPickupPoints")]
    public List<string> SupplierPickupPoints { get; set; } = new List<string>();

}
