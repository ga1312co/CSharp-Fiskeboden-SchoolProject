namespace Informatics.FiskebodenWebAPI.DTOs;

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
    

