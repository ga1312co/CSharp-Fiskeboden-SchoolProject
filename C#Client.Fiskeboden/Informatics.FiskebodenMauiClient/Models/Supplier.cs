namespace Informatics.FiskebodenMauiClient.Models;
using System.Text.Json.Serialization;


public class SupplierDto
{
    // Har exkluderat IDs (surrogate keys) från DTOs då Backend ska vara readonly

    public string SupplierNo { get; set; } = null!;

    public string SupplierName { get; set; } = null!;

    public string SupplierEmail { get; set; } = null!;

    public string? SupplierPhoneNumber { get; set; }

    public string? SupplierLocation { get; set; }

    public string SupplierSwishNumber { get; set; } = null!;

    // Sträng lista med batchnummer istället för batchobjekt
    public List<string> BatchNumbers { get; set; } = new List<string>();

    // Sträng lista med pickup point namn istället för pickup point objekt
    public List<string> PickupPointNames { get; set; } = new List<string>();
    
}

public record class Supplier
{
    [JsonPropertyName("SupplierNo")]
    public string SupplierNo { get; init; } = null!;

    [JsonPropertyName("SupplierName")]
    public string SupplierName { get; init; } = null!;

    [JsonPropertyName("SupplierEmail")]
    public string SupplierEmail { get; init; } = null!;

    [JsonPropertyName("SupplierPhoneNumber")]
    public string? SupplierPhoneNumber { get; init; }

    [JsonPropertyName("SupplierLocation")]
    public string? SupplierLocation { get; init; }

    [JsonPropertyName("SupplierSwishNumber")]
    public string SupplierSwishNumber { get; init; } = null!;

    [JsonPropertyName("BatchNumbers")]
    public List<string> BatchNumbers { get; init; } = new List<string>();

    [JsonPropertyName("PickupPointNames")]
    public List<string> PickupPointNames { get; init; } = new List<string>();
    
}
