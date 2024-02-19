using System.Text.Json.Serialization;

namespace AssurantTest.Application.Entities
{
    public record Promotion
    (
        [property: JsonPropertyName("id")]
        Guid Id,
        [property: JsonPropertyName("discountPercentage")]
        decimal DiscountPercentage,
        [property: JsonPropertyName("validFrom")]
        DateTime ValidFrom,
        [property: JsonPropertyName("validTo")]
        DateTime ValidTo
    );
}
