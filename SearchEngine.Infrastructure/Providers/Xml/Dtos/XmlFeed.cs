using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearchEngine.Infrastructure.Providers.Xml.Dtos
{
    [XmlRoot("feed")]
    public class XmlFeed
    {
        [XmlElement("items")]
        public XmlItems Items { get; set; } = new();

        [XmlElement("meta")]
        public XmlMeta Meta { get; set; } = new();
    }
}
