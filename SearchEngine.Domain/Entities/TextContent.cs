using SearchEngine.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Domain.Entities
{
    public class TextContent : ContentItem
    {
        public int Reactions { get; set; }
        public int ReadingTimeMinutes { get; set; }
    }
}
