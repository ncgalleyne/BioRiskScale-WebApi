using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class ResevoirHostRegionVM
    {
        public IEnumerable<ResevoirType> ResevoirTypes { get; set; }
        public IEnumerable<ResevoirEndemicity> ResevoirEndemicities { get; set; }
        public IEnumerable<HostEndemicity> HostEndemicities { get; set; }
        public Metrics Metrics { get; set; }
        public Recommendations Recs { get; set; }

        //public IEnumerable<Countries> Countries { get; set; }
        //public IEnumerable<Agents> Agents { get; set; }
    }
}