using System.Text.Json.Serialization;

namespace AssurantTest.Application.Entities
{
    public record State
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
