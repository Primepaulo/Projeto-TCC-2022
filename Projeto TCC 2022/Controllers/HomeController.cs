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
    /* Adiciona item para todos os controllers que o herdam.
    Ideia original de herdar um controller que herda outro controller retirada de: 
    https://stackoverflow.com/questions/27308524/access-viewbag-property-on-all-views
    public class SearchController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Request.QueryString["navSearch"].IsEmpty())
            {
                string searchTerm = Request.QueryString["navSearch"];
                ViewBag.Oficinas = Model1.GetOficina(searchTerm);
            }
        }
    }*/
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Lista = Model1.GetAllCarros();
            ViewBag.Item1 = Model1.GetAllCarros().FirstOrDefault();
            string userId = User.Identity.GetUserId();
            Debug.WriteLine(userId);
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

        public ActionResult InsertPessoa()
        {
            [HttpPost]
            Model1.InsertCelular(User.Identity.GetUserId(), Request["Celular"]);
            Model1.InsertPessoa(User.Identity.GetUserId(), Request["Nome"], Request["Sobrenome"], Request["Estado"]
                , Request["Cidade"], Request["Rua"], Convert.ToInt32(Request["Número"]), Request["Complemento"], 
                User.Identity.GetUserId(), Request["Email"], Request["CPF"], Request["CNPJ"], Convert.ToInt32(Request["Tipo"]));

            //Mudar pra acomodar ou físico ou jurídico

            return View();
        }
    }
}