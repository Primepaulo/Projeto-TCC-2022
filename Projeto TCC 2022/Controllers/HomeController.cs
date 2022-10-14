using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
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
            return View();
        }

        public ActionResult Search()
        {
            try
            {
                if (!Request.QueryString["navSearch"].IsEmpty())
                {
                    string searchTerm = Request.QueryString["n'avSearch"];
                    ViewBag.Oficina = Model1.SearchOficina(searchTerm);
                    
                }
                else
                {
                    ViewBag.Oficina = new Oficina();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
            }

            return View();
        }
    }
}