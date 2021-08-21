using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class GHSIVM
    {
        public IEnumerable<Prevention> Preventions { get; set; }
        public IEnumerable<Detection> Detections { get; set; }
        public IEnumerable<RapidResponse> Responses { get; set; }
        public IEnumerable<HealthSystem> Systems { get; set; }
        public IEnumerable<Compliance> Compliances { get; set; }
        public IEnumerable<RiskEnvironment> Environments { get; set; }
        public Recommendations Recs { get; set; }
        public Metrics Metrics { get; set; }
    }
}