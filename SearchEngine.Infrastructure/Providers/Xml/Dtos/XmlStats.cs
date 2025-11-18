using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SearchEngine.Infrastructure.Providers.Xml.Dtos
{
    public class XmlStats
    {
        [XmlElement("views")]
        public int Views { get; set; }

        [XmlElement("likes")]
        public int Likes { get; set; }

        [XmlElement("duration")]
        public string? Duration { get; set; }

        [XmlElement("reading_time")]
        public int? ReadingTime { get; set; }

        [XmlElement("reactions")]
        public int? Reactions { get; set; }
    }
}
