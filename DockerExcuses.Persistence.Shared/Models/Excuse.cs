using System.Text.Json.Serialization;

namespace DockerExcuses.Persistence.Shared.Models;

public class Excuse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;
}