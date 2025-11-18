using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearchEngine.Infrastructure.Providers.Xml.Dtos
{
    public class XmlMeta
    {
        [XmlElement("total_count")]
        public int TotalCount { get; set; }

        [XmlElement("current_page")]
        public int CurrentPage { get; set; }

        [XmlElement("items_per_page")]
        public int ItemsPerPage { get; set; }
    }
}
