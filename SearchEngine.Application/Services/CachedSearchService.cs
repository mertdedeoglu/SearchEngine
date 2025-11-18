using SearchEngine.Application.Dtos;
using SearchEngine.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Application.Services
{
    public class CachedSearchService : ISearchService
    {
        private readonly ISearchService _innerService;
        private readonly ICacheService _cacheService;

        public CachedSearchService(ISearchService innerService,ICacheService cacheService)
        {
            _innerService = innerService;
            _cacheService = cacheService;
        }

        public async Task<SearchResultDto> SearchAsync(SearchRequestDto request, CancellationToken cancellationToken = default)
        {
            string key = $"search:{request.Query}:{request.TypeFilter}:{request.SortBy}:{request.Page}:{request.PageSize}";

            var cached = _cacheService.Get<SearchResultDto>(key);
            if (cached is not null)
                return cached;

            var result = await _innerService.SearchAsync(request, cancellationToken);

            _cacheService.Set(key, result, TimeSpan.FromSeconds(30));

            return result;
        }
    }
}
