using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using System.Web.Routing;
using System.Web.WebPages;
using Microsoft.AspNet.Identity;
using Projeto_TCC_2022.Models;
using Projeto_TCC_2022.Models.STRUCT;

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

        [HttpGet]
        public ActionResult DataRequestSessions()
        {
            var searchData = new SearchData();
            searchData.searchTerm = Request.QueryString["navSearch"];
            searchData.filtro = Request.QueryString["filter"];
            searchData.categoria = Request.QueryString["categoria"];
            Session["searchData"] = searchData;
            return RedirectToAction("Search");
        }


        public ActionResult Search()
        {
            var searchData = Session["searchData"] as SearchData;
            Debug.WriteLine(searchData);

            if (HttpContext.Session["searchData"] != null)
            {
                if (!searchData.searchTerm.IsEmpty())
                {
                    if (!searchData.filtro.IsEmpty())
                    {
                        if (searchData.filtro == "OficinaNome")
                        {
                            ViewBag.Oficinas = Model1.GetOficina(searchData.searchTerm);
                        }

                        else if (searchData.filtro == "ServiçoCategoria")
                        {
                            var Serviços = Model1.GetServiçosByNomeFilterByCategoria(searchData.categoria, searchData.searchTerm);
                            ViewBag.Serviços = Serviços;

                            List<Oficina> oficinas = new List<Oficina>();

                            if (ViewBag.Serviços != null)
                            {
                                foreach (var item in Serviços)
                                {
                                    var oficina = Model1.GetOficinaById(item.Fk_Oficina_Id);
                                    oficinas.Add(oficina);
                                }

                                ViewBag.Oficinas = oficinas;
                            }
                        }

                        else if (searchData.filtro == "OficinaBairro")
                        {
                            ViewBag.Oficinas = Model1.GetOficinaByBairro(searchData.searchTerm);
                        }
                    }

                    List<Imagem> Imagens = new List<Imagem>();

                    if (ViewBag.Oficinas != null)
                    {
                        foreach (var oficina in ViewBag.Oficinas)
                        {
                            Imagens.Add(Model1.GetImagem(oficina.Id));
                        }
                    }

                    List<Avaliação> Avaliações = new List<Avaliação>();

                    if (ViewBag.Oficinas != null)
                    {
                        foreach (var oficina in ViewBag.Oficinas)
                        {
                            //Avaliações.Add(Model1.GetAvaliações(oficina.Id));
                        }
                    }

                    ViewBag.Imagens = Imagens;
                    ViewBag.Avaliações = Avaliações;
                }
            }

            return View();
        }

    }
}