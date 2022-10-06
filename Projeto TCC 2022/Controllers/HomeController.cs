using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto_TCC_2022.Models;

namespace Projeto_TCC_2022.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "Vende-se";
            ViewBag.Message = "Relação de carros";
            ViewBag.Lista = Model1.SearchAllCarros();
            ViewBag.Item1 = Model1.SearchAllCarros().FirstOrDefault();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}