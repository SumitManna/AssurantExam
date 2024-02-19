using System.Text.Json.Serialization;

namespace AssurantTest.Application.Entities
{
    public record Coupon
    (
        [property: JsonPropertyName("id")]
        Guid Id,
        [property: JsonPropertyName("productId")]
        Guid ProductId,
        [property: JsonPropertyName("code")]
        string Code,
        [property: JsonPropertyName("validFrom")]
        DateTime ValidFrom,
        [property: JsonPropertyName("validTo")]
        DateTime ValidTo,
        [property: JsonPropertyName("discountPercentage")]
        decimal DiscountPercentage
    );
}
