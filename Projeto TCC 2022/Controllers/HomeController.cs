using Projeto_TCC_2022.Models;
using Projeto_TCC_2022.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;

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
            if (ViewBag.éPessoa == true)
            {
                return RedirectToAction("IndexPessoa", "Home");
            }
            if (ViewBag.éOficina == true)
            {
                return RedirectToAction("IndexOficina", "Home");
            }

            ViewBag.Logado = User.Identity.IsAuthenticated;
            return View();
        }

        public ActionResult IndexPessoa()
        {
            if (ViewBag.éAdmin == true)
            {
                return RedirectToAction("Index", "Administrador");
            }
            if (ViewBag.éOficina == true)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult IndexOficina()
        {
            if (ViewBag.éAdmin == true)
            {
                return RedirectToAction("Index", "Administrador");
            }
            if (ViewBag.éPessoa == true)
            {
                return RedirectToAction("Index", "Home");
            }

            Oficina oficina = Model1.GetOficinaById(UserID);

            return View(oficina);
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
            List<Oficina> oficinas = new List<Oficina>();

            if (HttpContext.Session["searchData"] != null)
            {
                if (!searchData.searchTerm.IsEmpty())
                {
                    if (!searchData.filtro.IsEmpty())
                    {
                        if (searchData.filtro == "Oficina")
                        {
                            if (searchData.importado == true)
                            {
                                oficinas = Model1.GetOficinaByNameOrDescImp(searchData.importado, searchData.searchTerm);
                                Debug.WriteLine("Filtro 1 Importado");
                            }

                            else
                            {
                                oficinas = Model1.GetOficinaByNameOrDesc(searchData.searchTerm);
                                Debug.WriteLine("Filtro 1 Normal");
                            }

                        }

                        else if (searchData.filtro == "Serviço")
                        {
                            List<Serviço> Serviços = new List<Serviço>();

                            if (searchData.categoria == "Categoria")
                            {
                                Serviços = Model1.GetServiçosByInput(searchData.searchTerm);
                                Debug.WriteLine("Filtro 2 N Cat");
                            }

                            else if (searchData.importado == true)
                            {
                                Serviços = Model1.GetServiçosFilterByCategoriaImp(searchData.categoria, searchData.searchTerm);
                                Debug.WriteLine("Filtro 2 Imp");
                            }

                            else if (searchData.importado == false)
                            {
                                Serviços = Model1.GetServiçosFilterByCategoria(searchData.categoria, searchData.searchTerm);
                                Debug.WriteLine("Filtro 2");
                            }

                            ViewBag.Serviços = Serviços;


                            if (ViewBag.Serviços != null)
                            {
                                foreach (var item in Serviços)
                                {
                                    var oficina = Model1.GetOficinaById(item.Fk_Oficina_Id);
                                    oficinas.Add(oficina);
                                }
                            }
                        }

                        else if (searchData.filtro == "Bairro")
                        {
                            if (searchData.importado == true)
                            {
                                oficinas = Model1.GetOficinaByNameOrDescImp(searchData.importado, searchData.searchTerm);
                                Debug.WriteLine("Filtro 3 Importado");
                            }

                            else
                            {
                                oficinas = Model1.GetOficinaByBairro(searchData.searchTerm);
                                Debug.WriteLine("Filtro 3 Normal");

                            }
                        }
                    }
                }

                else if (searchData.searchTerm.IsEmpty() && searchData.filtro == "Serviço")
                {
                    if (searchData.categoria != "Categoria")
                    {
                        var Serviços = Model1.GetServiçosByCategoria(searchData.categoria);
                        foreach (var serviço in Serviços)
                        {
                            oficinas.Add(Model1.GetOficinaById(serviço.Fk_Oficina_Id));
                        }
                    }
                }

                List<Imagem> Imagens = new List<Imagem>();
                
                List<MédiasId> Médias = new List<MédiasId>();

                if (oficinas != null)
                {
                    foreach (var oficina in oficinas)
                    {
                        Imagens.Add(Model1.GetImagem(oficina.Id));
                        
                        double MédiaGeral = 0;
                        List<Avaliação> Av = Model1.GetAvaliaçãoByOficinaId(oficina.Id);

                        if (Av != null)
                        {
                            foreach (var item in Av)
                            {
                                MédiaGeral += item.Estrelas;
                            }

                            MédiaGeral /= Av.Count();

                            MédiasId média = new MédiasId
                            {
                                Média = MédiaGeral,
                                Oficina = oficina
                            };

                            Médias.Add(média);
                        }
                    }
                }

                Search search = new Search
                {
                    Oficinas = Médias.OrderByDescending(e => e.Média).ToList(),
                    Imagens = Imagens
                };

                return View(search);
            }

            return View();
        }

        public PartialViewResult NotificaçõesPartial()
        {
            List<Notificação> notificações = Model1.GetNotificações(UserID);
            return PartialView(notificações);
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