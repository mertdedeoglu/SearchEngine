using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearchEngine.Infrastructure.Providers.Xml.Dtos
{
    public class XmlItems
    {
        [XmlElement("item")]
        public List<XmlItem> ItemList { get; set; } = new();
    }
}
