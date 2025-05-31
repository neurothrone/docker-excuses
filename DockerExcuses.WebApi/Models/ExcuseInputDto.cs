using System.Text.Json.Serialization;

namespace DockerExcuses.WebApi.Models;

public record ExcuseInputDto
{
    [JsonPropertyName("text")]
    public required string Text { get; init; }

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;
}