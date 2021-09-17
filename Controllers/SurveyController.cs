using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using WebApplication4;
using WebApplication4.Controllers;
using WebApplication4.Models;

namespace WebApplication1.Controllers
{
    public class SurveyController : BaseController
    {
        //Bio_Risk_ScaleEntities masterdb = new Bio_Risk_ScaleEntities();//database connection
        
        // GET: RSH
        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Citations()
        {
            baseModel.Metrics = GetCitationMetrics();
            baseModel.Sources = GetCitationSources();
            return View(baseModel);
        }

        //load results for specfic user
        public ActionResult Results(string uid)
        {
            baseModel.Data = LoadResults(uid);
            return View(baseModel);
        }

        public List<SurveyData> LoadResults(string uid)
        {
            List<SurveyData> list = masterdb.Survey_History.Where(i => i.user_id == uid)
                .Select( i => new SurveyData()
                {
                    ReservoirType = i.Resevoir_Type,
                    agent_relatives = i.Uncertainty_of_Agent_Relatives,
                    agent_species = i.Uncertainty_of_Agent_Species,
                    awareness = i.Public_Awareness,
                    cfr = i.CFR,
                    Compliance = i.Compliance,
                    ContactRate = i.prevention,
                    DegreeOfContact = i.Degree_of_contact,
                    Detection = i.Detection,
                    DrugTreatment = i.Drug_treatment,
                    Environment = i.Environment,
                    Fear = i.Symptom_induced_fear,
                    GenerationTime = i.Generation_time,
                    History = i.History,
                    HostEndimicity = i.Host_Endemicity,
                    ifr = i.IFR,
                    Immunity = i.Duration_of_immunity,
                    Overlap = i.Infection_illness_overlap,
                    Persistance = i.Environmental_persistance,
                    ReservoirEndemicity = i.Resevoir_Endemicity,
                    Response = i.response,
                    System = i.System,
                    Date = i.C_Date,
                    PossibleModes = i.Number_of_possible_modes
                }).ToList();
            return list;
        }

        public ActionResult AddSurveyResults(int agent_relatives, int agent_species, int awareness, int cfr,
                                        int compliance, int contacts, int detection, int drug_treatment, int environment,
                                        int fear, int generation, int history, int host_type, int ifr,
                                        int immunity, int modes, int overlap, int persistance, int prevention,
                                        int reservoir_endimicity, int reservoir_type, int response, int system, string userId, int vaccine_treatment)
        {
            try
            {
                masterdb.AddSurvey_Results(compliance, reservoir_type, reservoir_endimicity, host_type,contacts, modes, persistance,
                                           overlap, drug_treatment, vaccine_treatment, generation, immunity, cfr, ifr, history, awareness,
                                           fear, agent_species, agent_relatives, detection, system, environment, prevention, response, userId, DateTime.Now);
                masterdb.SaveChanges();
                return RedirectToAction("Results", userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public ActionResult Index(string countryName, string agentName)
        {
            ResevoirHostRegionVM myModel = new ResevoirHostRegionVM();
            myModel.ResevoirTypes = GetResevoirTypes();
            myModel.ResevoirEndemicities = GetResevoirEndimicities();
            myModel.HostEndemicities = GetHostEndimicities();
            myModel.Recs = GetRHRRecs(countryName, agentName);
            myModel.Metrics = GetRHRMetrics();
            return PartialView("_ResevoirHostRegion", myModel);
        }
        public ActionResult Transmission(string countryName, string agentName)
        {
            TransmissionVM model = new TransmissionVM();
            model.InfectivityOverlaps = GetInfectivityOverlap();
            model.Contact = GetContact();
            model.EnvironmentalPersistance = GetEnvironmentalPersistance();
            model.PossibleModes = GetPossibleModes();
            model.Recs = GetTransmissionRecs(countryName, agentName);
            model.Metrics = GetTransmissionMetrics();
            return PartialView("_Transmission", model);
        }

        public ActionResult GHSI(string countryName, string agentName)
        {
            GHSIVM model = new GHSIVM();
            model.Compliances = GetCompliances();
            model.Detections = GetDetections();
            model.Environments = GetEnvironments();
            model.Preventions = GetPreventions();
            model.Responses = GetRapidResponses();
            model.Systems = GetHealthSystems();
            model.Metrics = GetGHSIMetrics();
            return PartialView("_GHSI", model);
        }
        public ActionResult DrugVaccines(string countryName, string agentName)
        {
            DrugVaccinesVM model = new DrugVaccinesVM();
            model.DrugTreatments = GetDrugTreatments();
            model.VaccineTreatments = GetVaccineTreatments();
            model.Recs = GetDrugVaccineRecs(countryName, agentName);
            model.Metrics = GetDrugVaccineMetrics();
            return PartialView("_DrugsVaccines", model);
        }
        public ActionResult BioClinical(string countryName, string agentName)
        {
            BioClinicalVM model = new BioClinicalVM();
            model.CFRs = GetCFRs();
            model.GenerationTimes = GetGenerationTimes();
            model.IFRs = GetIFRs();
            model.ImmunityDurations = GetImmunityDurations();
            model.Recs = GetBioClinicalRecs(countryName, agentName);
            model.Metrics = GetBioClinicalMetrics();
            return PartialView("_BioClinical", model);
        }
        public ActionResult Psych(string countryName, string agentName)
        {
            PyschVM model = new PyschVM();
            model.AgentRelatives = GetAgentRelatives();
            model.AgentSpecies = GetAgentSpecies();
            model.History = GetHistory();
            model.PublicAwarenesses = GetPublicAwarenesses();
            model.SymptomInducedFears = GetSymptomInducedFears();
            model.Recs = GetPsychRecs(countryName, agentName);
            model.Metrics = GetPsychMetrics();
            return PartialView("_Psych", model);
        }
        public Metrics GetGHSIMetrics()
        {
            Metrics m = new Metrics();
            Compliance c = masterdb.Metric_Info.Where(i => i.Metric_Id == 23)
                .Select(i => new Compliance()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            c.MetricSources = GetMetricSources(c.MetricId);
            c.AgentSources = GetAgentSources(c.MetricId);
            m.Compliance = c;
            Detection d = masterdb.Metric_Info.Where(i => i.Metric_Id == 20)
                .Select(i => new Detection()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            d.MetricSources = GetMetricSources(d.MetricId);
            d.AgentSources = GetAgentSources(d.MetricId);
            m.Detection = d;
            RiskEnvironment re = masterdb.Metric_Info.Where(i => i.Metric_Id == 24)
                .Select(i => new RiskEnvironment()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            re.MetricSources = GetMetricSources(re.MetricId);
            re.AgentSources = GetAgentSources(re.MetricId);
            m.RiskEnvironment = re;
            Prevention p = masterdb.Metric_Info.Where(i => i.Metric_Id == 19)
                .Select(i => new Prevention()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            p.MetricSources = GetMetricSources(p.MetricId);
            p.AgentSources = GetAgentSources(p.MetricId);
            m.Prevention = p;
            RapidResponse r = masterdb.Metric_Info.Where(i => i.Metric_Id == 24)
                .Select(i => new RapidResponse()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            r.MetricSources = GetMetricSources(r.MetricId);
            r.AgentSources = GetAgentSources(r.MetricId);
            m.RapidResponse = r;
            HealthSystem h = masterdb.Metric_Info.Where(i => i.Metric_Id == 24)
                .Select(i => new HealthSystem()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            h.MetricSources = GetMetricSources(h.MetricId);
            h.AgentSources = GetAgentSources(h.MetricId);
            m.HealthSystem = h;
            return m;
        }
        public Metrics GetRHRMetrics()
        {
            Metrics m = new Metrics();
            ResevoirType type = masterdb.Metric_Info.Where(i => i.Metric_Id == 16)
                .Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new ResevoirType()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            type.MetricSources = GetMetricSources(type.MetricId);
            type.AgentSources = GetAgentSources(type.MetricId);
            ResevoirEndemicity re = masterdb.Metric_Info
                .Where(j => j.Metric_Id == 15)
                .Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new ResevoirEndemicity()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                }).FirstOrDefault();
            re.MetricSources = GetMetricSources(re.MetricId);
            re.AgentSources = GetAgentSources(re.MetricId);
            HostEndemicity he = masterdb.Metric_Info
                .Where(j => j.Metric_Id == 15)
                .Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new HostEndemicity()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                }).FirstOrDefault();
            he.MetricSources = GetMetricSources(he.MetricId);
            he.AgentSources = GetAgentSources(he.MetricId);
            m.ResevoirType = type;
            m.ResevoirEndemicity = re;
            m.HostEndemicity = he;
            return m;
        }

        private List<AgentSource> GetAgentSources(int metricId)
        {
            List<AgentSource> ms = masterdb.Metric_Info.Where(i => i.Metric_Id == metricId)
                .Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new AgentSource()
                {
                    AgentCitation = j.Agent_Citation,
                    AgentLabel = j.Agent_Source_Name,
                    AgentLink = j.Agent_Source_Link
                }).Where(j => j.AgentLabel != "")
                .ToList();
            return ms;
        }

        private List<MetricSource> GetMetricSources(int metricId)
        {
            List<MetricSource> ms = masterdb.Metric_Info.Where(i => i.Metric_Id == metricId)
                .Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new MetricSource()
                {
                    MetricLabel = j.Metric_Source_Name,
                    MetricCitation = j.Metric_Citation,
                    MetricLink = j.Metric_Source_Link
                }).Where(j => j.MetricLabel != "")
                .ToList();
            return ms;
        }

        public Recommendations GetRHRRecs(string country, string agent)
        {
            Recommendations m = new Recommendations();
            ResevoirType rt = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
                .Select(i => new ResevoirType
                {
                    Host = i.Resevoir_Type
                }).FirstOrDefault();
            ResevoirEndemicity re = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
                .Select(
                i => new ResevoirEndemicity
                {
                    Host = i.Resevoir_Endemicity
                }).FirstOrDefault();
            HostEndemicity he = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
                .Select(
                i => new HostEndemicity
                {
                    Host = i.Host_Endemicity
                }).FirstOrDefault();
            m.ResevoirType = rt;
            m.ResevoirEndemicity = re;
            m.HostEndemicity = he;
            return m;
        }
        public Recommendations GetTransmissionRecs(string country, string agent)
        {
            Recommendations m = new Recommendations();
            DegreeOfContact dt = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
                .Select(i => new DegreeOfContact
                {
                    Host = i.Contact_Rate
                }).FirstOrDefault();
            PossibleModes pm = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
                .Select(
                i => new PossibleModes
                {
                    Host = i.Number_of_possible_modes
                }).FirstOrDefault();
            EnvironmentalPersistance ep = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
                .Select(
                i => new EnvironmentalPersistance
                {
                    Host = i.Environmental_persistance
                }).FirstOrDefault();
            InfectivityOverlap io = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
                .Select(
                i => new InfectivityOverlap
                {
                    Host = i.Infection_illness_overlap
                }).FirstOrDefault();
            m.DegreeOfContact = dt;
            m.PossibleModes = pm;
            m.EnvironmentalPersistance = ep;
            m.InfectivityOverlap = io;
            return m;
        }
        public Metrics GetTransmissionMetrics()
        {
            Metrics m = new Metrics();
            DegreeOfContact type = masterdb.Metric_Info.Where(i => i.Metric_Id == 2)
                .Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new DegreeOfContact()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            type.MetricSources = GetMetricSources(type.MetricId);
            type.AgentSources = GetAgentSources(type.MetricId);
            PossibleModes pm = masterdb.Metric_Info.Where(j => j.Metric_Id == 11)
                .Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new PossibleModes()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            pm.MetricSources = GetMetricSources(pm.MetricId);
            pm.AgentSources = GetAgentSources(pm.MetricId);
            EnvironmentalPersistance ep = masterdb.Metric_Info.Where(j => j.Metric_Id == 4).
                Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new EnvironmentalPersistance()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            ep.MetricSources = GetMetricSources(ep.MetricId);
            ep.AgentSources = GetAgentSources(ep.MetricId);
            InfectivityOverlap io = masterdb.Metric_Info.Where(j => j.Metric_Id == 10).
                Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new InfectivityOverlap()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            io.MetricSources = GetMetricSources(io.MetricId);
            io.AgentSources = GetAgentSources(io.MetricId);
            m.PossibleModes = pm;
            m.DegreeOfContact = type;
            m.InfectivityOverlap = io;
            m.EnvironmentalPersistance = ep;
            return m;
        }
        public Recommendations GetGHSIRecs(string country, string agent)
        {
            Recommendations m = new Recommendations();
            //RapidResponse rt = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
            //    .Select(i => new RapidResponse
            //    {
            //        Host = i
            //    }).FirstOrDefault();
            //ResevoirEndemicity re = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
            //    .Select(
            //    i => new ResevoirEndemicity
            //    {
            //        Host = i.Resevoir_Endemicity
            //    }).FirstOrDefault();
            //HostEndemicity he = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
            //    .Select(
            //    i => new HostEndemicity
            //    {
            //        Host = i.Host_Endemicity
            //    }).FirstOrDefault();
            //m.ResevoirType = rt;
            //m.ResevoirEndemicity = re;
            //m.HostEndemicity = he;
            return m;
        }
        public Recommendations GetBioClinicalRecs(string country, string agent)
        {
            Recommendations m = new Recommendations();
            GenerationTime gt = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
                .Select(i => new GenerationTime
                {
                    Host = i.Generation_time
                }).FirstOrDefault();
            ImmunityDuration id = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
                .Select(
                i => new ImmunityDuration
                {
                    Host = i.Duration_of_immunity
                }).FirstOrDefault();
            CFR cf = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
                .Select(
                i => new CFR
                {
                    Host = i.Case_Fatality_Ratio
                }).FirstOrDefault();
            IFR ifr = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
                .Select(
                i => new IFR
                {
                    Host = i.IFR
                }).FirstOrDefault();
            m.GenerationTime = gt;
            m.ImmunityDuration = id;
            m.CaseFatalityRatio = cf;
            m.InfantFatalityRatio = ifr;
            return m;
        }
        public Metrics GetBioClinicalMetrics()
        {
            Metrics m = new Metrics();
            GenerationTime dt = masterdb.Metric_Info.Where(j => j.Metric_Id == 7).
                Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new GenerationTime()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            dt.MetricSources = GetMetricSources(dt.MetricId);
            dt.AgentSources = GetAgentSources(dt.MetricId);
            ImmunityDuration pm = masterdb.Metric_Info.Where(j => j.Metric_Id == 3).
                Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new ImmunityDuration()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            pm.MetricSources = GetMetricSources(pm.MetricId);
            pm.AgentSources = GetAgentSources(pm.MetricId);
            CFR ep = masterdb.Metric_Info.Where(j => j.Metric_Id == 0).
                Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new CFR()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            ep.MetricSources = GetMetricSources(ep.MetricId);
            ep.AgentSources = GetAgentSources(ep.MetricId);
            IFR io = masterdb.Metric_Info.Where(j => j.Metric_Id == 9).
                Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new IFR()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            io.MetricSources = GetMetricSources(io.MetricId);
            io.AgentSources = GetAgentSources(io.MetricId);
            m.GenerationTime = dt;
            m.ImmunityDuration = pm;
            m.CaseFatalityRatio = ep;
            m.InfantFatalityRatio = io;
            return m;
        }
        public Recommendations GetDrugVaccineRecs(string country, string agent)
        {
            Recommendations m = new Recommendations();
            DrugTreatment dt = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
                .Select(i => new DrugTreatment
                {
                    Host = i.Drug_treatment
                }).FirstOrDefault();
            VaccineTreatment vt = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
                .Select(
                i => new VaccineTreatment
                {
                    Host = i.Vaccine_Treatment
                }).FirstOrDefault();
            m.DrugTreatment = dt;
            //m.vaccinetrearment = vt;
            return m;
        }
        public Metrics GetDrugVaccineMetrics()
        {
            Metrics m = new Metrics();
            DrugTreatment dt = masterdb.Metric_Info.Where(j => j.Metric_Id == 6).
                Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new DrugTreatment()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            dt.MetricSources = GetMetricSources(dt.MetricId);
            dt.AgentSources = GetAgentSources(dt.MetricId);
            VaccineTreatment pm = masterdb.Metric_Info.Where(j => j.Metric_Id == 5).
                Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new VaccineTreatment()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            pm.MetricSources = GetMetricSources(pm.MetricId);
            pm.AgentSources = GetAgentSources(pm.MetricId);
            m.DrugTreatment = dt;
            m.VaccineTreatment = pm;
            return m;
        }
        public Recommendations GetPsychRecs(string country, string agent)
        {
            Recommendations m = new Recommendations();
            History h = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
                .Select(i => new History
                {
                    Host = i.History
                }).FirstOrDefault();
            AgentSpecies a = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
                .Select(
                i => new AgentSpecies
                {
                    Host = i.Uncertainty_of_Agent_Species
                }).FirstOrDefault();
            AgentRelatives ar = masterdb.C_Geography.Where(j => j.Country_Name == country && j.Agent == agent)
                .Select(
                i => new AgentRelatives
                {
                    Host = i.Uncertainty_of_Agent_Relatives
                }).FirstOrDefault();
            m.History = h;
            m.AgentSpecies = a;
            m.Agent_Relatives = ar;
            return m;
        }
        public Metrics GetPsychMetrics()
        {
            Metrics m = new Metrics();
            History dt = masterdb.Metric_Info.Where(j => j.Metric_Id == 8).
                Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new History()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            dt.MetricSources = GetMetricSources(dt.MetricId);
            dt.AgentSources = GetAgentSources(dt.MetricId);
            AgentSpecies pm = masterdb.Metric_Info.Where(j => j.Metric_Id == 14).
                Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new AgentSpecies()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            pm.MetricSources = GetMetricSources(pm.MetricId);
            pm.AgentSources = GetAgentSources(pm.MetricId);
            AgentRelatives ep = masterdb.Metric_Info.Where(j => j.Metric_Id == 13).
                Join(masterdb.Metric_Sources, i => i.Metric_Name, j => j.Metric_Name, (i, j) => new AgentRelatives()
                {
                    Name = i.Metric_Name,
                    Description = i.Metric_Description,
                    MetricId = i.Metric_Id
                }).FirstOrDefault();
            ep.MetricSources = GetMetricSources(ep.MetricId);
            ep.AgentSources = GetAgentSources(ep.MetricId);
            m.History = dt;
            m.AgentSpecies = pm;
            m.Agent_Relatives = ep;
            return m;
        }

        private List<ResevoirType> GetResevoirTypes()
        {
            List<ResevoirType> types = new List<ResevoirType>();
            var list = masterdb.Resevoir_Host_Regions.Where(p => p.Resevoir_Type != null && p.Resevoir_Type != "").
                Select(p => new { p.Host, p.Resevoir_Type }).ToList();
            foreach (var type in list)
            {
                ResevoirType thisType = new ResevoirType();
                thisType.Host = type.Host;
                thisType.Name = type.Resevoir_Type;
                types.Add(thisType);
            }
            return types;
        }
        private List<DegreeOfContact> GetContact()
        {
            List<DegreeOfContact> types = new List<DegreeOfContact>();
            var list = masterdb.Transmissions.Where(p => p.Degree_of_Contact != null && p.Degree_of_Contact != "").
                Select(p => new { p.Host, p.Degree_of_Contact }).ToList();
            foreach (var type in list)
            {
                DegreeOfContact thisType = new DegreeOfContact();
                thisType.Host = type.Host;
                thisType.Name = type.Degree_of_Contact;
                types.Add(thisType);
            }
            return types;
        }
        private List<InfectivityOverlap> GetInfectivityOverlap()
        {
            List<InfectivityOverlap> types = new List<InfectivityOverlap>();
            var list = masterdb.Transmissions.Where(p => p.Infectivity_Overlap != null && p.Infectivity_Overlap != "").
                Select(p => new { p.Host, p.Infectivity_Overlap }).ToList();
            foreach (var type in list)
            {
                InfectivityOverlap thisType = new InfectivityOverlap();
                thisType.Host = type.Host;
                thisType.Name = type.Infectivity_Overlap;
                types.Add(thisType);
            }
            return types;
        }
        private List<EnvironmentalPersistance> GetEnvironmentalPersistance()
        {
            List<EnvironmentalPersistance> types = new List<EnvironmentalPersistance>();
            var list = masterdb.Transmissions.Where(p => p.Environmental_Persistence != null && p.Environmental_Persistence != "").
                Select(p => new { p.Host, p.Environmental_Persistence }).ToList();
            foreach (var type in list)
            {
                EnvironmentalPersistance thisType = new EnvironmentalPersistance();
                thisType.Host = type.Host;
                thisType.Name = type.Environmental_Persistence;
                types.Add(thisType);
            }
            return types;
        }
        private List<PossibleModes> GetPossibleModes()
        {
            List<PossibleModes> types = new List<PossibleModes>();
            var list = masterdb.Transmissions.Where(p => p.Number_of_possible_modes != null && p.Number_of_possible_modes != "").
                Select(p => new { p.Host, p.Number_of_possible_modes }).ToList();
            foreach (var type in list)
            {
                PossibleModes thisType = new PossibleModes();
                thisType.Host = type.Host;
                thisType.Name = type.Number_of_possible_modes;
                types.Add(thisType);
            }
            return types;
        }
        private List<AgentRelatives> GetAgentRelatives()
        {
            List<AgentRelatives> types = new List<AgentRelatives>();
            var list = masterdb.Psyches.Where(p => p.Uncertainty_of_Agent_Relatives != null && p.Uncertainty_of_Agent_Relatives != "").
                Select(p => new { p.Host, p.Uncertainty_of_Agent_Relatives }).ToList();
            foreach (var type in list)
            {
                AgentRelatives thisType = new AgentRelatives();
                thisType.Host = type.Host;
                thisType.Name = type.Uncertainty_of_Agent_Relatives;
                types.Add(thisType);
            }
            return types;
        }
        private List<AgentSpecies> GetAgentSpecies()
        {
            List<AgentSpecies> types = new List<AgentSpecies>();
            var list = masterdb.Psyches.Where(p => p.Uncertainty_of_Agent_Species != null && p.Uncertainty_of_Agent_Species != "").
                Select(p => new { p.Host, p.Uncertainty_of_Agent_Species }).ToList();
            foreach (var type in list)
            {
                AgentSpecies thisType = new AgentSpecies();
                thisType.Host = type.Host;
                thisType.Name = type.Uncertainty_of_Agent_Species;
                types.Add(thisType);
            }
            return types;
        }
        private List<History> GetHistory()
        {
            List<History> types = new List<History>();
            var list = masterdb.Psyches.Where(p => p.History != null && p.History != "").
                Select(p => new { p.Host, p.History }).ToList();
            foreach (var type in list)
            {
                History thisType = new History();
                thisType.Host = type.Host;
                thisType.Name = type.History;
                types.Add(thisType);
            }
            return types;
        }
        private List<PublicAwareness> GetPublicAwarenesses()
        {
            List<PublicAwareness> types = new List<PublicAwareness>();
            var list = masterdb.Psyches.Where(p => p.Public_Awareness != null && p.Public_Awareness != "").
                Select(p => new { p.Host, p.Public_Awareness }).ToList();
            foreach (var type in list)
            {
                PublicAwareness thisType = new PublicAwareness();
                thisType.Host = type.Host;
                thisType.Name = type.Public_Awareness;
                types.Add(thisType);
            }
            return types;
        }
        private List<SymptomInducedFear> GetSymptomInducedFears()
        {
            List<SymptomInducedFear> types = new List<SymptomInducedFear>();
            var list = masterdb.Psyches.Where(p => p.Symptom_induced_fear != null && p.Symptom_induced_fear != "").
                Select(p => new { p.Host, p.Symptom_induced_fear }).ToList();
            foreach (var type in list)
            {
                SymptomInducedFear thisType = new SymptomInducedFear();
                thisType.Host = type.Host;
                thisType.Name = type.Symptom_induced_fear;
                types.Add(thisType);
            }
            return types;
        }
        private List<CFR> GetCFRs()
        {
            List<CFR> types = new List<CFR>();
            var list = masterdb.Bio_Clinical.Where(p => p.CFR != null && p.CFR != "").
                Select(p => new { p.Host, p.CFR }).ToList();
            foreach (var type in list)
            {
                CFR thisType = new CFR();
                thisType.Host = type.Host;
                thisType.Name = type.CFR;
                types.Add(thisType);
            }
            return types;
        }
        private List<GenerationTime> GetGenerationTimes()
        {
            List<GenerationTime> types = new List<GenerationTime>();
            var list = masterdb.Bio_Clinical.Where(p => p.Generation_Time != null && p.Generation_Time != "").
                Select(p => new { p.Host, p.Generation_Time }).ToList();
            foreach (var type in list)
            {
                GenerationTime thisType = new GenerationTime();
                thisType.Host = type.Host;
                thisType.Name = type.Generation_Time;
                types.Add(thisType);
            }
            return types;
        }
        private List<IFR> GetIFRs()
        {
            List<IFR> types = new List<IFR>();
            var list = masterdb.Bio_Clinical.Where(p => p.IFR != null && p.IFR != "").
                Select(p => new { p.Host, p.IFR }).ToList();
            foreach (var type in list)
            {
                IFR thisType = new IFR();
                thisType.Host = type.Host;
                thisType.Name = type.IFR;
                types.Add(thisType);
            }
            return types;
        }
        private List<ImmunityDuration> GetImmunityDurations()
        {
            List<ImmunityDuration> types = new List<ImmunityDuration>();
            var list = masterdb.Bio_Clinical.Where(p => p.Duration_Immunity != null && p.Duration_Immunity != "").
                Select(p => new { p.Host, p.Duration_Immunity }).ToList();
            foreach (var type in list)
            {
                ImmunityDuration thisType = new ImmunityDuration();
                thisType.Host = type.Host;
                thisType.Name = type.Duration_Immunity;
                types.Add(thisType);
            }
            return types;
        }
        private List<DrugTreatment> GetDrugTreatments()
        {
            List<DrugTreatment> types = new List<DrugTreatment>();
            var list = masterdb.Drugs_Vaccines.Where(p => p.Drug_treatment != null && p.Drug_treatment != "").
                Select(p => new { p.Host, p.Drug_treatment }).ToList();
            foreach (var type in list)
            {
                DrugTreatment thisType = new DrugTreatment();
                thisType.Host = type.Host;
                thisType.Name = type.Drug_treatment;
                types.Add(thisType);
            }
            return types;
        }
        private List<VaccineTreatment> GetVaccineTreatments()
        {
            List<VaccineTreatment> types = new List<VaccineTreatment>();
            var list = masterdb.Drugs_Vaccines.Where(p => p.Vaccine_treatment != null && p.Vaccine_treatment != "").
                Select(p => new { p.Host, p.Vaccine_treatment }).ToList();
            foreach (var type in list)
            {
                VaccineTreatment thisType = new VaccineTreatment();
                thisType.Host = type.Host;
                thisType.Name = type.Vaccine_treatment;
                types.Add(thisType);
            }
            return types;
        }
        private List<Compliance> GetCompliances()
        {
            List<Compliance> types = new List<Compliance>();
            var list = masterdb.GHSIs.Where(p => p.Compliance != null && p.Compliance != "").
                Select(p => new { p.Host, p.Compliance }).ToList();
            foreach (var type in list)
            {
                Compliance thisType = new Compliance();
                thisType.Host = type.Host;
                thisType.Name = type.Compliance;
                types.Add(thisType);
            }
            return types;
        }
        private List<Detection> GetDetections()
        {
            List<Detection> types = new List<Detection>();
            var list = masterdb.GHSIs.Where(p => p.Detection != null && p.Detection != "").
                Select(p => new { p.Host, p.Detection }).ToList();
            foreach (var type in list)
            {
                Detection thisType = new Detection();
                thisType.Host = type.Host;
                thisType.Name = type.Detection;
                types.Add(thisType);
            }
            return types;
        }
        private List<RiskEnvironment> GetEnvironments()
        {
            List<RiskEnvironment> types = new List<RiskEnvironment>();
            var list = masterdb.GHSIs.Where(p => p.Risk_Environment != null && p.Risk_Environment != "").
                Select(p => new { p.Host, p.Risk_Environment }).ToList();
            foreach (var type in list)
            {
                RiskEnvironment thisType = new RiskEnvironment();
                thisType.Host = type.Host;
                thisType.Name = type.Risk_Environment;
                types.Add(thisType);
            }
            return types;
        }
        private List<Prevention> GetPreventions()
        {
            List<Prevention> types = new List<Prevention>();
            var list = masterdb.GHSIs.Where(p => p.Prevention != null && p.Prevention != "").
                Select(p => new { p.Host, p.Prevention }).ToList();
            foreach (var type in list)
            {
                Prevention thisType = new Prevention();
                thisType.Host = type.Host;
                thisType.Name = type.Prevention;
                types.Add(thisType);
            }
            return types;
        }
        private List<RapidResponse> GetRapidResponses()
        {
            List<RapidResponse> types = new List<RapidResponse>();
            var list = masterdb.GHSIs.Where(p => p.Rapid_Response != null && p.Rapid_Response != "").
                Select(p => new { p.Host, p.Rapid_Response }).ToList();
            foreach (var type in list)
            {
                RapidResponse thisType = new RapidResponse();
                thisType.Host = type.Host;
                thisType.Name = type.Rapid_Response;
                types.Add(thisType);
            }
            return types;
        }
        private List<HealthSystem> GetHealthSystems()
        {
            List<HealthSystem> types = new List<HealthSystem>();
            var list = masterdb.GHSIs.Where(p => p.Health_System != null && p.Health_System != "").
                Select(p => new { p.Host, p.Health_System }).ToList();
            foreach (var type in list)
            {
                HealthSystem thisType = new HealthSystem();
                thisType.Host = type.Host;
                thisType.Name = type.Health_System;
                types.Add(thisType);
            }
            return types;
        }

        private List<ResevoirEndemicity> GetResevoirEndimicities()
        {
            List<ResevoirEndemicity> endemicities = new List<ResevoirEndemicity>();
            var list = masterdb.Resevoir_Host_Regions.Where(p => p.Resevoir_Endemicity != null && p.Resevoir_Endemicity != "").
                Select(p => new { p.Host, p.Resevoir_Endemicity }).ToList();
            foreach (var endemicity in list)
            {
                ResevoirEndemicity re = new ResevoirEndemicity();
                re.Host = endemicity.Host;
                re.Name = endemicity.Resevoir_Endemicity;
                endemicities.Add(re);
            }
            return endemicities;
        }
        private List<HostEndemicity> GetHostEndimicities()
        {
            List<HostEndemicity> endemicities = new List<HostEndemicity>();
            var list = masterdb.Resevoir_Host_Regions.Where(p => p.Host_Endemicity != null && p.Host_Endemicity != "").
                Select(p => new { p.Host, p.Host_Endemicity }).ToList();
            foreach (var type in list)
            {
                HostEndemicity thisType = new HostEndemicity();
                thisType.Host = type.Host;
                thisType.Name = type.Host_Endemicity;
                endemicities.Add(thisType);
            }
            return endemicities;
        }

        private Metrics GetCitationMetrics()
        {
            Metrics m = new Metrics();
            m.MetricObjs = getmetricobjs();
            return m;
        }
        public List<SourceRecord> GetCitationSources()
        {
            List<SourceRecord> sourceRecords = masterdb.Bibliographies.Select(r => new SourceRecord()
            {
                Title = r.Source_Title,
                MetricName = r.Metric_Name,
                AgentName = r.Agent_Name,
                Citation = r.Source_Citation,
                Link = r.Source_Link
            }).ToList();
            return sourceRecords;
        }

        private List<MetricObj> getmetricobjs()
        {
            List<MetricObj> objList = new List<MetricObj>();
            for (int i = 0; i < 24; i++)
            {
                MetricObj obj = new MetricObj();
                obj.MetricName = GetMetricName(i);
                obj.MetricSources = GetMetricSources(i);
                obj.AgentSources = GetAgentSources(i);
                objList.Add(obj);
            }
            return objList;
        }

        private string GetMetricName(int id)
        {
            string name = masterdb.Metric_Info.Where(i => i.Metric_Id == id)
                .Select(i => i.Metric_Name).FirstOrDefault();
            return name;
        }
        private Object getobj(object o)
        {
            switch (o.GetType().ToString())
            {
                case "CFR":
                    CFR c = new CFR();
                    c.MetricSources = GetMetricSources(0);
                    c.AgentSources = GetAgentSources(0);
                    return c;
                case "DegreeOfContact":
                    DegreeOfContact a = new DegreeOfContact();
                    a.MetricSources = GetMetricSources(2);
                    a.AgentSources = GetAgentSources(2);
                    return a;
                case "ImmunityDuration":
                    ImmunityDuration b = new ImmunityDuration();
                    b.MetricSources = GetMetricSources(3);
                    b.AgentSources = GetAgentSources(3);
                    return b;
                case "EnvironmentalPersistance":
                    EnvironmentalPersistance ep = new EnvironmentalPersistance();
                    ep.MetricSources = GetMetricSources(4);
                    ep.AgentSources = GetAgentSources(4);
                    return ep;
                case "VaccineTreatment":
                    VaccineTreatment d = new VaccineTreatment();
                    d.MetricSources = GetMetricSources(5);
                    d.AgentSources = GetAgentSources(5);
                    return d;
                case "DrugTreatment":
                    DrugTreatment e = new DrugTreatment();
                    e.MetricSources = GetMetricSources(6);
                    e.AgentSources = GetAgentSources(6);
                    return e;
                case "GenerationTime":
                    GenerationTime f = new GenerationTime();
                    f.MetricSources = GetMetricSources(7);
                    f.AgentSources = GetAgentSources(7);
                    return f;
                case "History":
                    History g = new History();
                    g.MetricSources = GetMetricSources(8);
                    g.AgentSources = GetAgentSources(8);
                    return g;
                case "IFR":
                    IFR h = new IFR();
                    h.MetricSources = GetMetricSources(9);
                    h.AgentSources = GetAgentSources(9);
                    return h;
                case "InfectivityOverlap":
                    InfectivityOverlap i = new InfectivityOverlap();
                    i.MetricSources = GetMetricSources(10);
                    i.AgentSources = GetAgentSources(10);
                    return i;
                case "PossibleModes":
                    PossibleModes j = new PossibleModes();
                    j.MetricSources = GetMetricSources(11);
                    j.AgentSources = GetAgentSources(11);
                    return j;
                //case "Transmission":
                //    Transmission k = new Transmission();
                //    k.MetricSources = GetMetricSources(12);
                //    k.AgentSources = GetAgentSources(12);
                //    return k;
                case "AgentRelatives":
                    AgentRelatives l = new AgentRelatives();
                    l.MetricSources = GetMetricSources(13);
                    l.AgentSources = GetAgentSources(13);
                    return l;
                case "AgentSpecies":
                    AgentSpecies m = new AgentSpecies();
                    m.MetricSources = GetMetricSources(14);
                    m.AgentSources = GetAgentSources(14);
                    return m;
                case "ResevoirEndemicity":
                    ResevoirEndemicity n = new ResevoirEndemicity();
                    n.MetricSources = GetMetricSources(15);
                    n.AgentSources = GetAgentSources(15);
                    return n;
                case "ResevoirType":
                    ResevoirType rt = new ResevoirType();
                    rt.MetricSources = GetMetricSources(16);
                    rt.AgentSources = GetAgentSources(16);
                    return rt;
                //case "PublicAwareness":
                //    PublicAwareness p = new PublicAwareness();
                //    p.MetricSources = GetMetricSources(17);
                //    p.AgentSources = GetAgentSources(17);
                //    return p;
                //case "SymptomInducedFear":
                //    SymptomInducedFear q = new SymptomInducedFear();
                //    q.MetricSources = GetMetricSources(18);
                //    q.AgentSources = GetAgentSources(18);
                //    return q;
                case "Prevention":
                    Prevention r = new Prevention();
                    r.MetricSources = GetMetricSources(19);
                    r.AgentSources = GetAgentSources(19);
                    return r;
                case "Detection":
                    Detection s = new Detection();
                    s.MetricSources = GetMetricSources(20);
                    s.AgentSources = GetAgentSources(20);
                    return s;
                case "RapidResponse":
                    CFR t = new CFR();
                    t.MetricSources = GetMetricSources(21);
                    t.AgentSources = GetAgentSources(21);
                    return t;
                case "HealthSystem":
                    HealthSystem u = new HealthSystem();
                    u.MetricSources = GetMetricSources(22);
                    u.AgentSources = GetAgentSources(22);
                    return u;
                case "Compliance":
                    Compliance v = new Compliance();
                    v.MetricSources = GetMetricSources(23);
                    v.AgentSources = GetAgentSources(23);
                    return v;
                case "RiskEnvironment":
                    RiskEnvironment w = new RiskEnvironment();
                    w.MetricSources = GetMetricSources(24);
                    w.AgentSources = GetAgentSources(24);
                    return w;
            }
            return o;
        }
    }
}