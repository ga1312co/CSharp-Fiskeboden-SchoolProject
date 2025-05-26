namespace Informatics.FiskebodenWebAPI.DTOs;

public class CategoryDto
{
    // Har exkluderat IDs (surrogate keys) från DTOs då Backend ska vara readonly
    public string CategoryNo { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    // Sträng lista med produktnamn istället för produktobjekt
    public List<string> ProductNames { get; set; } = new List<string>();

}
