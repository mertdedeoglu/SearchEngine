using SearchEngine.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Domain.Entities
{
    public class VideoContent : ContentItem
    {
        public int Views { get; set; }
        public int Likes { get; set; }
    }
}
