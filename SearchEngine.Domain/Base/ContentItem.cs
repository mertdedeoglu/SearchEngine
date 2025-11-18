using SearchEngine.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Domain.Base
{
    public abstract class ContentItem
    {
        public Guid Id { get; set; }
        public string ProviderName { get; set; }
        public string ProviderItemId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public ContentType Type { get; set; }
        public DateTime PublishedTime { get; set; }
        public double FinalScore { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
