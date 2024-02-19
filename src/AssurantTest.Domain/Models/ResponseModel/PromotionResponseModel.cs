using System.Text.Json.Serialization;

namespace AssurantTest.Domain.Models.ResponseModel
{
    public record PromotionResponseModel
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
