using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class PyschVM
    {
        public IEnumerable<History> History { get; set; }
        public IEnumerable<PublicAwareness> PublicAwarenesses { get; set; }
        public IEnumerable<SymptomInducedFear> SymptomInducedFears { get; set; }
        public IEnumerable<AgentSpecies> AgentSpecies { get; set; }
        public IEnumerable<AgentRelatives> AgentRelatives { get; set; }
        public Metrics Metrics { get; set; }
        public Recommendations Recs { get; set; }
    }
}