using System.Text.Json.Serialization;

namespace BupaAustraliaCodingTest.Models
{
    public sealed class OwnerDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("books")]
        public List<BookDto> Books { get; set; } = [];
    }
}
