using SearchEngine.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Application.Interfaces
{
    public interface ISearchService
    {
        Task<SearchResultDto> SearchAsync(SearchRequestDto request, CancellationToken cancellationToken = default);
    }
}
