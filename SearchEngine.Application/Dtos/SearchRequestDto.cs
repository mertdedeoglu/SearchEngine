using SearchEngine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Application.Dtos
{
    public class SearchRequestDto
    {
        public string Query { get; set; } = string.Empty;
        public ContentType? TypeFilter { get; set; }
        public string? SortBy { get; set; } = "score";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
