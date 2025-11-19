using SearchEngine.Application.Interfaces;
using SearchEngine.Domain.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngine.Tests
{
    public class FakeContentQuery : IContentQuery
    {
        private readonly List<ContentItem> _items;

        public FakeContentQuery(List<ContentItem> items)
        {
            _items = items;
        }

        public IQueryable<ContentItem> Query()
        {
            return _items.AsQueryable();
        }

    }
}
