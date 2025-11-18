using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearchEngine.Infrastructure.Providers.Xml.Dtos
{
    public class XmlItem
    {
        [XmlElement("id")]
        public string Id { get; set; } = default!;

        [XmlElement("headline")]
        public string Headline { get; set; } = default!;

        [XmlElement("type")]
        public string Type { get; set; } = default!; // "video" | "article"

        [XmlElement("stats")]
        public XmlStats Stats { get; set; } = new();

        [XmlElement("publication_date")]
        public string PublicationDate { get; set; } = default!; // "2024-03-15"

        [XmlElement("categories")]
        public XmlCategories Categories { get; set; } = new();
    }
}
