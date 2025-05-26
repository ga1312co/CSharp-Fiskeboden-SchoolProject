namespace Informatics.FiskebodenWebAPI.DTOs;

public class PickupPointDto
{
    // Har exkluderat IDs (surrogate keys) från DTOs då Backend ska vara readonly

    public string PickupPointNo { get; set; } = null!;

    public string PickupPointAddress { get; set; } = null!;

    public string PickupPointName { get; set; } = null!;

    public List<string> SupplierPickupPoints { get; set; } = new List<string>();

}
