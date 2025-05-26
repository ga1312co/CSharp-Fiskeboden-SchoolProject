namespace Informatics.FiskebodenWebAPI.DTOs;

public class ProductDto
{
    // Har exkluderat IDs (surrogate keys) från DTOs då Backend ska vara readonly

    public string ProductNo { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public bool IsMeasuredInUnits { get; set; }

    public string CategoryName { get; set; } = null!;

    public List<string> BatchNumbers { get; set; } = new List<string>();

}
