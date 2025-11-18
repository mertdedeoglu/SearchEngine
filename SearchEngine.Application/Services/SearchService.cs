using Microsoft.EntityFrameworkCore;
using SearchEngine.Application.Dtos;
using SearchEngine.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Application.Services
{
    public class SearchService : ISearchService
    {
        private readonly IContentQuery _query;

        public SearchService(IContentQuery query)
        {
            _query = query;
        }

        public async Task<SearchResultDto> SearchAsync(SearchRequestDto request, CancellationToken cancellationToken = default)
        {
            //EF Core Tarafindan SQL
            var q = _query.Query();

            //1. Anahtar Kelimeye Gore Arama
            if(!string.IsNullOrWhiteSpace(request.Query))
            {
                var keyword = request.Query.Trim().ToLower();
                q = q.Where(x=> 
                    x.Title.ToLower().Contains(keyword) || 
                    x.Description.ToLower().Contains(keyword));
            }

            //2. Icerik turune gore filtre (video/metin)
            if (request.TypeFilter.HasValue)
                q = q.Where(x => x.Type == request.TypeFilter.Value);

            //3. Populerlik ve alakalilik skoruna gore siralama
            q = request.SortBy switch
            {
                "publishedTime" => q.OrderByDescending(x => x.PublishedTime),
                "score" => q.OrderByDescending(x => x.FinalScore)
            };

            //4. Pagination
            int total = await q.CountAsync(cancellationToken);

            var items = await q
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new SearchResultItemDto
            {
                Title = x.Title,
                Type = x.Type,
                Score = x.FinalScore,
                ProviderName = x.ProviderName,
                Url = x.Url,
                PublishedTime = x.PublishedTime
            })
            .ToListAsync(cancellationToken);

            return new SearchResultDto
            {
                TotalCount = total,
                Items = items
            };
        }
    }
}
