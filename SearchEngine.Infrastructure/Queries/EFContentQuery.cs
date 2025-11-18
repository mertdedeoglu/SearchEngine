using Microsoft.EntityFrameworkCore;
using SearchEngine.Application.Interfaces;
using SearchEngine.Domain.Base;
using SearchEngine.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Infrastructure.Queries
{
    public class EFContentQuery : IContentQuery
    {
        private readonly AppDbContext _db;

        public EFContentQuery(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<ContentItem> Query()
        {
            return _db.ContentItems.AsNoTracking();
        }
    }
}
