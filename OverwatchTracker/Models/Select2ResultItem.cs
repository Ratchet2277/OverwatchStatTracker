using System.Text.Json.Serialization;

namespace WebApplication.Models
{
    public class Select2ResultItem
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}