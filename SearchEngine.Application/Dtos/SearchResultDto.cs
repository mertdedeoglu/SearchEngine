using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Application.Dtos
{
    public class SearchResultDto
    {
        public IReadOnlyList<SearchResultItemDto> Items { get; set; } = Array.Empty<SearchResultItemDto>();
        public int TotalCount { get; set; }
    }
}
