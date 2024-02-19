using System.Text.Json.Serialization;

namespace AssurantTest.Domain.Models.ResponseModel
{
    public record ProductResponseModel
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
