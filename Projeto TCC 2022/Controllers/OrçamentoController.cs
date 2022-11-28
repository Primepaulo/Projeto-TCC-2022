using Projeto_TCC_2022.Models;
using Projeto_TCC_2022.Models.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class OrçamentoController : DefaultController
    {
        public ActionResult EscolhaCarro(int Id)
        {
            Response.Clear();
            Session.Clear();
            Session["OficinaId"] = Id;
            ViewBag.Carros = Model1.GetCarros(UserID);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EscolherCarro()
        {
            string Placa = Request.Form["carro"];
            bool Value = Convert.ToBoolean(Request.QueryString["Value"]);
            int Id = Convert.ToInt32(Session["OficinaId"]);

            Carro carro = Model1.GetCarro(Placa);
            Session["carro"] = carro;

            if (Value == false)
            {
                return RedirectToAction("AgendarServiço", "Orçamento", new { Id });
            }
            else if (Value == true)
            {
                return RedirectToAction("AgendarAnálise", "Orçamento", new { Id });
            }
            else
                return RedirectToAction("Erro"); //TBA
        }

        public ActionResult AgendarServiço(int Id)
        {
            if (Pessoa == true && Session["carro"] != null)
            {
                Oficina oficina = Model1.GetOficinaById(Id);
                List<Serviço> serviços = Model1.GetServiços(Id);
                List<Categoria> categorias = new List<Categoria>();

                foreach (Serviço serviço in serviços)
                {
                    Categoria categoria = Model1.GetCategoriaById(serviço.Fk_Categoria_Id);
                    if (!categorias.Contains(categoria))
                    {
                        categorias.Add(categoria);
                    }
                }

                ViewBag.CategoriasOficina = categorias;
                ViewBag.OrçamentoOficina = oficina;
                ViewBag.Carro = Session["carro"];
                return View();
            }

            return RedirectToAction("Erro"); //TBA
            // Eventualmente será necessário usar Session.Clear(); pra matar as SessionVariables Carro e OficinaId
        }
        
        public PartialViewResult OptionsServiço(int categoriaId, int oficinaId)
        {
            List<Serviço> serviços = Model1.GetServiçosByOficinaFilterByCategoria(categoriaId, oficinaId);
            ViewBag.ServiçoOficina = serviços;
            return PartialView();
        }


        public PartialViewResult ListItemPartial(int Id)
        {
            ServiçoCategoria serviçoCategoria = new ServiçoCategoria();
            Serviço serviço = Model1.GetServiço(Id);
            Categoria categoria = Model1.GetCategoriaById(serviço.Fk_Categoria_Id);

            serviçoCategoria.Serviço = serviço;
            serviçoCategoria.Categoria = categoria;

            ViewBag.ServiçoCategoria = serviçoCategoria;
            Session["ServiçoOrçamento"] = serviço.Preço;
            Debug.WriteLine(Session["ServiçoOrçamento"]);

            return PartialView();
        }

        public PartialViewResult TotalPartial()
        {
            decimal soma = 0;

            decimal valor = Convert.ToDecimal(Session["ServiçoOrçamento"]);

            if (Session["Soma"] == null)
            {
                soma += valor;
            }
            else
            {
                soma = Convert.ToDecimal(Session["Soma"]) + valor;
            }
            Debug.WriteLine(Session["Soma"]);
            Session["Soma"] = soma;
            Debug.WriteLine(soma);
            ViewBag.Total = soma;
            return PartialView();
        }
    
    
    }
}
