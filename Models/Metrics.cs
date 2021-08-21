using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.Models;

namespace WebApplication1.Models
{
    public class Metrics
    {
        public IEnumerable<MetricObj> MetricObjs {get; set; }
        public CFR CaseFatalityRatio { get; set; }
        public AgentRelatives Agent_Relatives { get; set; }
        public AgentSpecies AgentSpecies { get; set; }
        public Compliance Compliance { get; set; }
        public DegreeOfContact DegreeOfContact { get; set; }
        public Detection Detection { get; set; }
        public DrugVaccine DrugsVaccines { get; set; }
        public DrugTreatment DrugTreatment { get; set; }
        public EnvironmentalPersistance EnvironmentalPersistance { get; set; }
        public GenerationTime GenerationTime { get; set; }
        public HealthSystem HealthSystem { get; set; }
        public History History { get; set; }
        public HostEndemicity HostEndemicity { get; set; }
        public IFR InfantFatalityRatio { get; set; }
        public ImmunityDuration ImmunityDuration { get; set; }
        public InfectivityOverlap InfectivityOverlap { get; set; }
        public PossibleModes PossibleModes { get; set; }
        public Prevention Prevention { get; set; }
        public RapidResponse RapidResponse { get; set; }
        public ResevoirEndemicity ResevoirEndemicity { get; set; }
        public ResevoirType ResevoirType { get; set; }
        public RiskEnvironment RiskEnvironment { get; set; }
        public VaccineTreatment VaccineTreatment { get; set; }
        //public _transmission CaseFatalityRatio { get; set; }
        //public contactrate CaseFatalityRatio { get; set; }
    }
}