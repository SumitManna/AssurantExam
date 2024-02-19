using System.Text.Json.Serialization;

namespace AssurantTest.Domain.Models.ResponseModel
{
    public record CustomerResponseModel
    (
        [property: JsonPropertyName("id")]
        Guid Id,
        [property: JsonPropertyName("stateId")]
        Guid StateId,
        [property: JsonPropertyName("name")]
        string Name
    );
}
