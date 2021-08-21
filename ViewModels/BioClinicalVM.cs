using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class BioClinicalVM
    {
        public IEnumerable<GenerationTime> GenerationTimes { get; set; }
        public IEnumerable<ImmunityDuration> ImmunityDurations { get; set; }
        public IEnumerable<CFR> CFRs { get; set; }
        public IEnumerable<IFR> IFRs { get; set; }
        public Recommendations Recs { get; set; }
        public Metrics Metrics { get; set; }
    }
}