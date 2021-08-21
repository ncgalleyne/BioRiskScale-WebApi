using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class MetricObj
    {
        public IEnumerable<MetricSource> MetricSources { get; set; }
        public IEnumerable<AgentSource> AgentSources { get; set; }
        public string MetricName { get; set; }
    }
}