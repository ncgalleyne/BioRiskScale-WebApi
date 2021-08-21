using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class TransmissionVM
    {
        public IEnumerable<DegreeOfContact> Contact { get; set; }
        public IEnumerable<PossibleModes> PossibleModes { get; set; }
        public IEnumerable<EnvironmentalPersistance> EnvironmentalPersistance { get; set; }
        public IEnumerable<InfectivityOverlap> InfectivityOverlaps { get; set; }
        public Metrics Metrics { get; set; }
        public Recommendations Recs { get; set; }
    }
}