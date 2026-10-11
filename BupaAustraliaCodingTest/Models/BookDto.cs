using System.Text.Json.Serialization;

namespace BupaAustraliaCodingTest.Models
{
    public sealed class BookDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
    }
}
