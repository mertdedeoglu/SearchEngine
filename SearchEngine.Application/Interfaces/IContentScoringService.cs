using SearchEngine.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Application.Interfaces
{
    public interface IContentScoringService
    {
        double CalculateScore(ContentItem item);
    }
}
