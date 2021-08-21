using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.ViewModels
{
    public class DrugVaccinesVM
    {
        public IEnumerable<DrugTreatment> DrugTreatments { get; set; }
        public IEnumerable<VaccineTreatment> VaccineTreatments { get; set; }
        public Metrics Metrics { get; set; }
        public Recommendations Recs { get; set; }
    }
}