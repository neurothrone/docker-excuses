using System.Text.Json.Serialization;

namespace DockerExcuses.Persistence.Shared.Models;

public record ExcuseInputDto
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }

    [JsonPropertyName("category")]
    public required string Category { get; init; }
}