using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication4.Models;

namespace WebApplication1.ViewModels
{
    public class SurveyVM
    {
        public IEnumerable<Countries> Countries { get; set; }
        public IEnumerable<Agents> Agents { get; set; }
        public Metrics Metrics { get; set; }
        public AccountUser AccountUser { get; set; }
        public IEnumerable<SurveyData> Data { get; set; }
        public IEnumerable<SourceRecord> Sources { get; set; }
    }
}