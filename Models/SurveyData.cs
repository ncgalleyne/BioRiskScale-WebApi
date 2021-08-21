using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class SurveyData
    {
        public int Compliance { get; set; }
        public int ReservoirType { get; set; }
        public int ReservoirEndemicity { get; set; }
        public int HostEndimicity { get; set; }
        public int ContactRate { get; set; }
        public int DegreeOfContact { get; set; }
        public int PossibleModes { get; set; }
        public int Persistance { get; set; }
        public int Overlap { get; set; }
        public int DrugTreatment { get; set; }
        public int VaccineTreatment { get; set; }
        public int GenerationTime { get; set; }
        public int Immunity { get; set; }
        public int cfr { get; set; }
        public int ifr { get; set; }
        public int History { get; set; }
        public int awareness { get; set; }
        public int Fear { get; set; }
        public int agent_species { get; set; }
        public int agent_relatives { get; set; }
        public int Detection { get; set; } 
        public int System { get; set; }
        public int Environment { get; set; }
        public int Response { get; set; }
        public int UserId { get; set; }
        public DateTime? Date { get; set; }
    }
}