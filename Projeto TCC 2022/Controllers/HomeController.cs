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

                if (!Request.QueryString["filter"].IsEmpty())
                {
                    if (Request.QueryString["filter"] == "OficinaNome")
                    {
                        ViewBag.Oficinas = Model1.GetOficina(searchTerm);
                    }

                    else if (Request.QueryString["filter"] == "ServiçoCategoria")
                    {
                        ViewBag.Oficinas = Model1.GetServiçosByCategoria(searchTerm);
                    }

                    else if (Request.QueryString["filter"] == "OficinaBairro")
                    {
                        ViewBag.Oficinas = Model1.GetOficinaByBairro(searchTerm);
                    }
                }

                List<Imagem> Imagens = new List<Imagem>();
                foreach (var oficina in ViewBag.Oficinas) {
                    Imagens.Add(Model1.GetImagem(oficina.Id));
                }

                List<Avaliação> Avaliações = new List<Avaliação>();
                foreach (var oficina in ViewBag.Oficinas)
                {
                    //Avaliações.Add(Model1.GetAvaliações(oficina.Id));
                }

                ViewBag.Imagens = Imagens;
                ViewBag.Avaliações = Avaliações;
            }

            return View();
        }
        
    }
}