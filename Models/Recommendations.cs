using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Recommendations
    {
        public CFR CaseFatalityRatio { get; set; }
        public AgentRelatives Agent_Relatives { get; set; }
        public AgentSpecies AgentSpecies { get; set; }
        public DegreeOfContact DegreeOfContact { get; set; }
        //public DrugVaccines DrugsVaccines { get; set; }
        public DrugTreatment DrugTreatment { get; set; }
        public EnvironmentalPersistance EnvironmentalPersistance { get; set; }
        public GenerationTime GenerationTime { get; set; }
        public History History { get; set; }
        public HostEndemicity HostEndemicity { get; set; }
        public IFR InfantFatalityRatio { get; set; }
        public ImmunityDuration ImmunityDuration { get; set; }
        public InfectivityOverlap InfectivityOverlap { get; set; }
        public PossibleModes PossibleModes { get; set; }
        public ResevoirEndemicity ResevoirEndemicity { get; set; }
        public ResevoirType ResevoirType { get; set; }
        //public _transmission CaseFatalityRatio { get; set; }
        //public contactrate CaseFatalityRatio { get; set; }
    }
}