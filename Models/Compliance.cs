using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.Models;

namespace WebApplication1.Models
{
    public class Compliance
    {
        public int Host { get; set; }
        public int MetricId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<MetricSource> MetricSources { get; set; }
        public IEnumerable<AgentSource> AgentSources { get; set; }
    }
}