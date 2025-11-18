using SearchEngine.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Application.Interfaces
{
    public interface IContentQuery
    {
        IQueryable<ContentItem> Query();

    }
}
