using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using System.Web.WebPages;
using Microsoft.AspNet.Identity;
using Projeto_TCC_2022.Models;

namespace Projeto_TCC_2022.Controllers
{
    public class HomeController : DefaultController
    {
        public ActionResult Index()
        {
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
                List<Imagem> Imagens = new List<Imagem>();
                foreach (var oficina in ViewBag.Oficinas) {
                    Imagens.Add(Model1.GetImagem(oficina.Id));
                }
                ViewBag.Imagens = Imagens;
            }
            return View();
        }
        
    }
}