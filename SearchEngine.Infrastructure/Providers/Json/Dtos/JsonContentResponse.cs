using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SearchEngine.Infrastructure.Providers.Json.Dtos
{
    public class JsonContentResponse
    {
        [JsonPropertyName("contents")]
        public List<JsonContentItem> Contents { get; set; } = new();

        [JsonPropertyName("pagination")]
        public JsonPagination Pagination { get; set; } = new();
    }
}
