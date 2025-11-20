using SearchEngine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Application.Dtos
{
    public class SearchResultItemDto
    {
        public string Title { get; set; } = default!;
        public ContentType Type { get; set; }
        public string TypeName { get; set; }
        public double Score { get; set; }
        public string ProviderName { get; set; } = default!;
        public string Url { get; set; } = default!;
        public DateTime PublishedTime { get; set; }
    }
}
