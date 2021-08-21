using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ResevoirHostRegion
    {
        public List<int> Host { get; set; }
        public List<string> ResevoirType { get; set; }
        public List<string> ResevoirEndemicity { get; set; }
        public List<string> HostEndemicity { get; set; }
    }
}