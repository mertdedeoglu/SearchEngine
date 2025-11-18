using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SearchEngine.Infrastructure.Providers.Json.Dtos
{
    public class JsonContentItem
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = default!;

        [JsonPropertyName("title")]
        public string Title { get; set; } = default!;

        [JsonPropertyName("type")]
        public string Type { get; set; } = default!; // "video" | "article"

        [JsonPropertyName("metrics")]
        public JsonMetrics Metrics { get; set; } = new();

        [JsonPropertyName("published_at")]
        public DateTime PublishedAt { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new();
    }
}
