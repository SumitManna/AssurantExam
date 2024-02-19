using System.Text.Json.Serialization;

namespace AssurantTest.Domain.Models.ResponseModel
{
    public record StateResponseModel
    (
        [property: JsonPropertyName("id")]
        Guid Id,
        [property: JsonPropertyName("code")]
        string Code,
        [property: JsonPropertyName("name")]
        string Name,
        [property: JsonPropertyName("taxRates")]
        decimal TaxRates,
        [property: JsonPropertyName("applyLuxuryTax")]
        bool ApplyLuxuryTax
    );
}
