using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Controllers
{
    public class HomeController : BaseController
    {
        //Bio_Risk_ScaleEntities masterdb = new Bio_Risk_ScaleEntities();
        public ActionResult Index()
        { 
            return View(baseModel);
        }

        [Authorize]
        public bool UpdateUserCredentials(string id, string fName, string lName, string email, string location, string occupation)
        {
            try
            {
                masterdb.UpdateUserCredentials(id, fName, lName, email, location, occupation);
                masterdb.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        [Authorize]
        public ActionResult Profiles(string id)
        {
            baseModel.AccountUser = masterdb.AspNetUsers.Where(i => i.UserName == id)
                .Select(j => new AccountUser()
                {
                    FirstName = j.FirstName,
                    LastName = j.LastName,
                    Email = j.Email,
                    Location = j.C_Location,
                    Occupation = j.Occupation
                }).FirstOrDefault();
            string uid = masterdb.AspNetUsers.Where(i => i.UserName == id).Select(j => j.Id).FirstOrDefault();
            baseModel.Data = LoadResults(uid);
            return View(baseModel);
        }
        public List<SurveyData> LoadResults(string uid)
        {
            List<SurveyData> list = masterdb.Survey_History.Where(i => i.user_id == uid)
                .Select(i => new SurveyData()
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


        public ActionResult Survey()
        {
            return View(baseModel);
        }
        public ActionResult Login(string email, string pwd)
        {
            string _password = ComputeSha256Hash(pwd).Substring(0, 50);
            AccountUser LoggedInUser = masterdb.User_Accounts.Where(p => p.Email == email && p.C_Password == _password).
                Select(p => new AccountUser
                {
                    FirstName = p.First_Name,
                    LastName = p.Last_Name,
                    Email = p.Email,
                    Location = p.C_Location,
                    Occupation = p.Occupation,
                    UserId = p.User_Id
                }).ToList().FirstOrDefault();
            ViewBag.UserName = LoggedInUser.FirstName;
            return Json(LoggedInUser, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateAccount(string f_name, string l_name, string email, string pwd, string loc, string occupation)
        {
            string _password = ComputeSha256Hash(pwd);
            int u_id = masterdb.User_Accounts.Count();
            masterdb.AddUser_Acct(u_id, f_name, l_name, email, _password, loc, occupation);
            return Login(email, pwd);
        }

        private string ComputeSha256Hash(string rawData)
        {
            //www.c-sharpcorner.com/article/compute-sha256-hash-in-c-sharp/
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
