using Microsoft.EntityFrameworkCore;
using SearchEngine.Application.Interfaces;
using SearchEngine.Domain.Providers;
using SearchEngine.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Infrastructure.Services
{
    public class ProviderSyncService
    {
        private readonly IEnumerable<IContentProvider> _providers;
        private readonly IContentScoringService _scoring;
        private readonly AppDbContext _db;
        private readonly ICacheService _cache;

        public ProviderSyncService(
            IEnumerable<IContentProvider> providers,
            IContentScoringService scoring,
            AppDbContext db,
            ICacheService cache)
        {
            _providers = providers;
            _scoring = scoring;
            _db = db;
            _cache = cache;
        }

        public async Task SyncAllAsync()
        {
            foreach (var provider in _providers)
            {
                var items = await provider.FetchAsync();

                foreach (var item in items)
                {
                    // Skor hesaplama
                    item.FinalScore = _scoring.CalculateScore(item);

                    // DB'de var mı kontrol
                    var existing = await _db.ContentItems
                        .FirstOrDefaultAsync(x => x.ProviderItemId == item.ProviderItemId && x.ProviderName == item.ProviderName);

                    if (existing == null)
                    {
                        // INSERT
                        await _db.ContentItems.AddAsync(item);
                    }
                    else
                    {
                        // UPDATE
                        existing.Title = item.Title;
                        existing.Description = item.Description;
                        existing.Type = item.Type;
                        existing.PublishedTime = item.PublishedTime;
                        existing.FinalScore = item.FinalScore;
                        existing.Url = item.Url;
                        existing.ProviderName = item.ProviderName;
                    }
                }
            }

            await _db.SaveChangesAsync();

        }
    }
}
