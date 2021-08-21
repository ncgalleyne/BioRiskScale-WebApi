using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication4.Models;

namespace WebApplication4.ViewModels
{
    public class AccountUserVM
    {
        public AccountUser AccountUser { get; set; }
        public IEnumerable<SurveyData> Data { get; set; }
    }
}