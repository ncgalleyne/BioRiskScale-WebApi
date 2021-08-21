using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PublicAwareness
    {
        public int Host { get; set; }
        public int MetricId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> MetricSource { get; set; }
        public List<string> AgentSources { get; set; }
        public List<string> MetricLink { get; set; }
        public List<string> AgentLink { get; set; }
    }

}