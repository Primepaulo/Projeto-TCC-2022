using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Microsoft.AspNet.Identity;
using Projeto_TCC_2022.Models;

namespace Projeto_TCC_2022.Controllers
{
    public class HomeController : DefaultController
    {
        public ActionResult Index()
        {
            ViewBag.Oficinas = Model1.GetAllOficinas();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Search()
        {
            if (!Request.QueryString["navSearch"].IsEmpty())
            {
                string searchTerm = Request.QueryString["navSearch"];
                ViewBag.Oficinas = Model1.GetOficina(searchTerm);
            }
            return View();
        }
        
    }
}