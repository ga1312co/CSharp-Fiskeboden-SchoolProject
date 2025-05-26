namespace Informatics.FiskebodenMauiClient.Models;
using System.Text.Json.Serialization;

public record class Category
{

    [JsonPropertyName("CategoryNo")]
    public string CategoryNo { get; set; } = null!;

    [JsonPropertyName("CategoryName")]
    public string CategoryName { get; set; } = null!;

    [JsonPropertyName("Products")]
    public List<Product> Products { get; set; } = new List<Product>();


}
