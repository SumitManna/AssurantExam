using System.Text.Json.Serialization;

namespace AssurantTest.Application.Entities
{
    public record Customer
    (
        [property: JsonPropertyName("id")]
        Guid Id,
        [property: JsonPropertyName("stateId")]
        Guid StateId,
        [property: JsonPropertyName("name")]
        string Name
    );
}
