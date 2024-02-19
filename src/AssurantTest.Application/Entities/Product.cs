using System.Text.Json.Serialization;

namespace AssurantTest.Application.Entities
{
    public record Product
    (
        [property: JsonPropertyName("id")]
        Guid Id,
        [property: JsonPropertyName("name")]
        string Name,
        [property: JsonPropertyName("price")]
        decimal Price,
        [property: JsonPropertyName("isLuxuryProduct")]
        bool IsLuxuryProduct
    );
}
