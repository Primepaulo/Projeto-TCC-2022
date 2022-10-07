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
            ViewBag.Lista = Model1.SearchAllCarros();
            ViewBag.Item1 = Model1.SearchAllCarros().FirstOrDefault();
            return View();
        }

        public ActionResult Contact()
        {
            try
            {
                if (!Request.QueryString["navSearch"].IsEmpty())
                {
                    int searchTerm = Convert.ToInt32(Request.QueryString["navSearch"]);
                    ViewBag.Pessoa = Model1.GetPessoa(searchTerm).FirstOrDefault();
                    
                }
                else
                {
                    ViewBag.Pessoa = new Pessoa();
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