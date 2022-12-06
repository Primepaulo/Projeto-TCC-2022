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

namespace Projeto_TCC_2022.Controllers
{
    public class HomeController : DefaultController
    {
        public ActionResult Index()
        {
            Response.Clear();
            Session.Clear();
            if (ViewBag.éAdmin == true)
            {
                return RedirectToAction("Index", "Administrador");
            }
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
            searchData.importado = Convert.ToBoolean(Request.QueryString["importado"]);
            
            Session["searchData"] = searchData;
            return RedirectToAction("Search");
        }

        public ActionResult Search()
        {
            var searchData = Session["searchData"] as SearchData;

            if (HttpContext.Session["searchData"] != null)
            {
                if (!searchData.searchTerm.IsEmpty())
                {
                    if (!searchData.filtro.IsEmpty())
                    {
                        if (searchData.filtro == "OficinaNome")
                        {
                            if (searchData.importado == true)
                            {
                                ViewBag.Oficinas = Model1.GetOficinaByNameOrDescImp(searchData.importado, searchData.searchTerm);
                                Debug.WriteLine("Filtro 1 Importado");
                            }

                            else
                            {
                                ViewBag.Oficinas = Model1.GetOficinaByNameOrDesc(searchData.searchTerm);
                                Debug.WriteLine("Filtro 1 Normal");
                            }

                        }

                        else if (searchData.filtro == "ServiçoCategoria")
                        {
                            var Serviços = Model1.GetServiçosFilterByCategoria(searchData.categoria, searchData.searchTerm);
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
                                Debug.WriteLine("Filtro 2");
                            }
                        }

                        else if (searchData.filtro == "OficinaBairro")
                        {
                            if (searchData.importado == true)
                            {
                                ViewBag.Oficinas = Model1.GetOficinaByNameOrDescImp(searchData.importado, searchData.searchTerm);
                                Debug.WriteLine("Filtro 3 Importado");
                            }

                            else
                            {
                                ViewBag.Oficinas = Model1.GetOficinaByBairro(searchData.searchTerm);
                                Debug.WriteLine("Filtro 3 Normal");

                            }
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

        public PartialViewResult NotificaçõesPartial()
        {
            ViewBag.Notificações = Model1.GetNotificações(UserID);
            return PartialView();
        }

        public ActionResult MarkAsRead(int Id)
        {
            var notificação = Model1.GetNotificação(Id);
            if (notificação.Fk_User_Id == UserID)
            {
                Model1.MarcarComoLido(notificação);
            }
            return null;
        }

    }
}