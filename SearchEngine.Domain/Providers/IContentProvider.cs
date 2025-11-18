using SearchEngine.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Domain.Providers
{
    public interface IContentProvider
    {
        string Name { get; }
        /// <summary>
        /// Provider'dan ham veriyi cekip normalize edilmis ContentItem turevlerini doner.
        /// </summary>
        Task<IReadOnlyList<ContentItem>> FetchAsync(CancellationToken cancellationToken = default);
    }
}
