using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class SymptomInducedFear
    {
        public int Host { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sources { get; set; }
        public List<string> AgentSources { get; set; }
    }
}