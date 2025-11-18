using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SearchEngine.Infrastructure.Providers.Json.Dtos
{
    public class JsonMetrics
    {
        [JsonPropertyName("views")]
        public int Views { get; set; }

        [JsonPropertyName("likes")]
        public int Likes { get; set; }

        [JsonPropertyName("reading_time")]
        public int ReadingTime { get; set; }

        [JsonPropertyName("reactions")]
        public int Reactions { get; set; }

        [JsonPropertyName("comments")]
        public int Comments { get; set; }

        [JsonPropertyName("duration")]
        public string Duration { get; set; } = default!;
    }
}
