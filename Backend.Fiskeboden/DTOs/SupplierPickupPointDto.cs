namespace Informatics.FiskebodenWebAPI.DTOs;

public class SupplierPickupPointDto
{
    public string SupplierName { get; set; } = null!;

    public string PickupPointName { get; set; } = null!;

    public byte PickupDay { get; set; }

    public TimeOnly PickupTime { get; set; }

}
