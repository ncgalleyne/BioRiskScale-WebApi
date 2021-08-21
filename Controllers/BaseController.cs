using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication4.Controllers
{
    public class BaseController : Controller
    {
        public SurveyVM baseModel = new SurveyVM();
        public Bio_Risk_ScaleEntities masterdb = new Bio_Risk_ScaleEntities();
        public BaseController()
        {
            baseModel.Countries = GetCountries();
            baseModel.Agents = GetAgents();
        }

        // GET: Base
        public ActionResult _Index()
        {
            baseModel.Countries = GetCountries();
            baseModel.Agents = GetAgents();
            return View();
        }
        private List<Countries> GetCountries()
        {
            List<Countries> list = masterdb.Country_Codes.Select(c => new Countries
            {
                Name = c.Country_Name,
                Id = c.Country_Id
            }).Distinct().ToList();
            return list;
        }
        private List<Agents> GetAgents()
        {
            List<Agents> list = masterdb.C_Geography.Select(j => new Agents
            {
                Name = j.Agent
            }).Distinct().ToList();
            return list;
        }
    }
}