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
        public ActionResult VisualizarOrçamentos()
        {
            List<Orçamento> orçamentos = Model1.GetOrçamentos(UserID);
            ViewBag.Orçamentos = orçamentos;
            List<Oficina> oficinas = new List<Oficina>();
            List<Carro> carros = new List<Carro>();

            foreach (var orçamento in orçamentos)
            {
                oficinas.Add(Model1.GetOficinaById(orçamento.fk_Oficina_Id));
                carros.Add(Model1.GetCarro(orçamento.fk_Carro_Placa));
            }
            ViewBag.Oficinas = oficinas;
            ViewBag.Carros = carros;
            
            return View();
        }
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
            Session["ServiçoOrçamento"] = null;
            Session["Soma"] = null;
            ViewBag.Total = null;

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
        }
        
        public PartialViewResult ServiçosDisponiveisPartial(int categoriaId, int oficinaId)
        {
            List<Serviço> serviços = Model1.GetServiçosByOficinaFilterByCategoria(categoriaId, oficinaId);
            ViewBag.ServiçoOficina = serviços;
            return PartialView();
        }


        public PartialViewResult ServiçoItemOrçamentoPartial(int Id)
        {
            ServiçoCategoria serviçoCategoria = new ServiçoCategoria();
            Serviço serviço = Model1.GetServiço(Id);
            Categoria categoria = Model1.GetCategoriaById(serviço.Fk_Categoria_Id);

            serviçoCategoria.Serviço = serviço;
            serviçoCategoria.Categoria = categoria;

            ViewBag.ServiçoCategoria = serviçoCategoria;
            Session["ServiçoOrçamento"] = serviço.Preço;

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
            Session["Soma"] = soma;
            ViewBag.Total = soma;
            return PartialView();
        }

        [HttpGet]
        public ActionResult LimparOrçamento()
        {
            Session["ServiçoOrçamento"] = null;
            Session["Soma"] = null;
            ViewBag.Total = null;
            return null;
        }

        [HttpPost]
        public ActionResult Submit(ServiçoTotal serviçoTotal)
        {
            decimal final = 0;

            foreach (string item in serviçoTotal.serviços)
            {
                Serviço serviço = Model1.GetServiço(Convert.ToInt32(item));
                final += serviço.Preço;
            }
            if (Session["OficinaId"] != null && Session["carro"] != null && serviçoTotal.Total != null && serviçoTotal.serviços != null)
            {
                decimal valor = Decimal.Parse(serviçoTotal.Total);
                if (final == valor)
                {
                    Carro carro = (Carro)Session["carro"];
                    int oficinaId = (int)Session["OficinaId"];
                    DateTime data = DateTime.Now;

                    Orçamento orçamento = Model1.CreateOrçamento(UserID, carro.Placa, oficinaId, data, valor);

                    foreach (string item in serviçoTotal.serviços)
                    {
                        int ServiçoId = Convert.ToInt32(item);
                        ItemOrçamento itemOrçamento = Model1.GetItemOrçamentoServiço(orçamento.Id, ServiçoId);
                        if (itemOrçamento == null)
                        {
                            Model1.AddItemOrçamento(orçamento.Id, ServiçoId, null, 1);
                        }
                        else
                        {
                            itemOrçamento.Quantidade += 1;
                            Model1.AddQuantidade(itemOrçamento);
                        }
                    }
                    return JavaScript($"window.location='/Orçamento/StatusOrçamentoPessoa/'");
                }
            }
            else if (Session["OficinaId"] == null)
            {
                return RedirectToAction("Erro");
                //Tivemos um erro quando tentamos buscar os dados da oficina, tente novamente
            }
            else if (Session["carro"] == null)
            {
                return RedirectToAction("Erro");
                //Tivemos um erro quando tentamos buscar os dados do carro escolhido, tente novamente
            }
            return View();
        }
    
        public ActionResult StatusOrçamentoPessoa(int Id)
        {
            Response.Clear();
            Session.Clear();
            ViewBag.OrçamentoId = Id;

            return View();
        }

        public PartialViewResult StatusOrçamentoPessoaPartial(int Id)
        {
            Orçamento orçamento = Model1.GetOrçamento(Id);
            ViewBag.Orçamento = orçamento;

            Oficina oficina = Model1.GetOficinaById(orçamento.fk_Oficina_Id);
            ViewBag.Oficina = oficina;

            string[] Horarios = oficina.HorarioFuncionamento.Split('/');
            ViewBag.Horarios = Horarios;

            return PartialView();
        }


    }
}
