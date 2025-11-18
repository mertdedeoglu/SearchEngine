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
                    item.FinalScore = _scoring.CalculateScore(item);
                }

                await _db.AddRangeAsync(items);
            }

            await _db.SaveChangesAsync();

        }
    }
}
