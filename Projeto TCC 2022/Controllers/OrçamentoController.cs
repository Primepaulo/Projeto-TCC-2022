using Projeto_TCC_2022.Models;
using Projeto_TCC_2022.Models.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Projeto_TCC_2022.Controllers
{
    public class OrçamentoController : DefaultController
    {
        public ActionResult VisualizarOrçamentos()
        {
            
            return View();
        }
        public PartialViewResult VisualizarOrçamentoPessoaPartial()
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
            return PartialView();
        }

        public PartialViewResult VisualizarOrçamentoOficinaPartial()
        {
            List<Orçamento> orçamentos = Model1.GetOrçamentos(UserID);
            ViewBag.Orçamentos = orçamentos;
            List<Pessoa> pessoas = new List<Pessoa>();
            List<Carro> carros = new List<Carro>();

            foreach (var orçamento in orçamentos)
            {
                pessoas.Add(Model1.GetPessoa(orçamento.fk_Pessoa_Id));
                carros.Add(Model1.GetCarro(orçamento.fk_Carro_Placa));
            }
            ViewBag.Pessoas = pessoas;
            ViewBag.Carros = carros;
            return PartialView();
        }


        public ActionResult EscolhaCarro(int Id, bool Value)
        {
            Response.Clear();
            Session.Clear();
            Session["OficinaId"] = Id;
            ViewBag.Carros = Model1.GetCarros(UserID);
            TempData["Value"] = Value;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EscolherCarro(bool Value)
        {
            string Placa = Request.Form["carro"];
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

        public ActionResult AgendarAnálise(int Id)
        {
            if (Session["carro"] != null && Pessoa == true && Session["OficinaId"] != null)
            {
                Carro carro = (Carro)Session["carro"];
                DateTime data = DateTime.Now;
                Orçamento orçamento = Model1.CreateOrçamento(UserID, carro.Placa, Id, data, null, 2);

                return RedirectToAction("StatusOrçamentoPessoa", "Orçamento", new { orçamento.Id });
            }

            else
                return RedirectToAction("Erro");
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

        public PartialViewResult ItemOrçamentoPartial(int Id, int? Tipo)
        {
            if (Tipo == 1 || Tipo == null)
            {
                ServiçoCategoria serviçoCategoria = new ServiçoCategoria();
                Serviço serviço = Model1.GetServiço(Id);
                Categoria categoria = Model1.GetCategoriaById(serviço.Fk_Categoria_Id);

                serviçoCategoria.Serviço = serviço;
                serviçoCategoria.Categoria = categoria;

                ViewBag.ServiçoCategoria = serviçoCategoria;
                Session["ServiçoOrçamento"] = serviço.Preço;
                ViewBag.Tipo = 1;
            }

            else if (Tipo == 2)
            {
                var peça = Model1.GetPeça(Id);
                ViewBag.Peça = peça;
                Session["ServiçoOrçamento"] = peça.Preço;
                ViewBag.Tipo = 2;
            }

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
        public ActionResult LimparOrçamento(int? Type)
        {
            if (Type == null)
            {
                Session["ServiçoOrçamento"] = null;
                Session["Soma"] = null;
                ViewBag.Total = null;
            }
            else
            {
                decimal Valor = (decimal)Session["ValorOriginal"];
                Session["Soma"] = Valor;
                ViewBag.Total = Valor;
            }
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

                    Orçamento orçamento = Model1.CreateOrçamento(UserID, carro.Placa, oficinaId, data, valor, 1);

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

            Orçamento orçamento = Model1.GetOrçamento(Id);
            if (Pessoa == true && orçamento.fk_Pessoa_Id == UserID)
            {
                ViewBag.OrçamentoId = Id;
                return View();
            }

            return RedirectToAction("Erro");
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

        public ActionResult StatusOrçamentoOficina(int Id)
        {
            Orçamento orçamento = Model1.GetOrçamento(Id);
            if (ViewBag.éOficina == true && orçamento.fk_Oficina_Id == UserID)
            {
                Response.Clear();
                Session.Clear();

                ViewBag.Orçamento = orçamento;
                ViewBag.Carro = Model1.GetCarro(orçamento.fk_Carro_Placa);
                ViewBag.Pessoa = Model1.GetPessoa(orçamento.fk_Pessoa_Id);

                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ConfirmarDeletarOrçamento(int Id, int Operação)
        {
            Orçamento orçamento = Model1.GetOrçamento(Id);
            if (ViewBag.éOficina == true && orçamento.fk_Oficina_Id == UserID && Operação == 1)
            {
                return RedirectToAction("AdicionarPeçaServiço/" + orçamento.Id);

            }

            return View();
        }

        public ActionResult AdicionarPeçaServiço (int Id)
        {
            Session["ServiçoOrçamento"] = null;
            Session["Soma"] = null;
            ViewBag.Total = null;
            Orçamento orçamento = Model1.GetOrçamento(Id);

            if (Oficina == true && orçamento.fk_Oficina_Id == UserID)
            {
                Oficina oficina = Model1.GetOficinaById(UserID);
                List<Serviço> serviços = Model1.GetServiços(UserID);
                List<Peça> peças = Model1.GetPeças(UserID);
                List<Categoria> categorias = new List<Categoria>();

                foreach (Serviço serviço in serviços)
                {
                    Categoria categoria = Model1.GetCategoriaById(serviço.Fk_Categoria_Id);
                    if (!categorias.Contains(categoria))
                    {
                        categorias.Add(categoria);
                    }
                }

                if (orçamento.Tipo == 1)
                {
                    Session["Soma"] = orçamento.Valor;
                    ViewBag.Total = Session["Soma"];

                    Session["ValorOriginal"] = Session["Soma"];
                }

                ViewBag.CategoriasOficina = categorias;
                ViewBag.OrçamentoOficina = oficina;
                ViewBag.PeçaOficina = peças;
                ViewBag.Orçamento = orçamento;

                return View();
            }

            return RedirectToAction("Erro"); //TBA
        }

        [HttpPost]
        public ActionResult Update(ServiçoPeçaTotal serviçoPeçaTotal)
        {
            decimal final = 0;
            Orçamento orçamento = Model1.GetOrçamento(serviçoPeçaTotal.Id);

            if (orçamento.Tipo == 2)
            {
                foreach (string item in serviçoPeçaTotal.serviços)
                {
                    Serviço serviço = Model1.GetServiço(Convert.ToInt32(item));
                    final += serviço.Preço;

                    ItemOrçamento itemOrçamento = Model1.GetItemOrçamentoServiço(orçamento.Id, serviço.Id);

                    if (itemOrçamento == null)
                    {
                        Model1.AddItemOrçamento(orçamento.Id, serviço.Id, null, 1);
                    }
                    else
                    {
                        itemOrçamento.Quantidade += 1;
                        Model1.AddQuantidade(itemOrçamento);
                    }

                }
            }

            foreach (string item in serviçoPeçaTotal.peças)
            {
                Peça peça = Model1.GetPeça(Convert.ToInt32(item));
                ItemOrçamento itemOrçamento = Model1.GetItemOrçamentoPeça(orçamento.Id, peça.Id);
                if (itemOrçamento == null)
                {
                    Model1.AddItemOrçamento(orçamento.Id, null, peça.Id, 1);
                }
                else
                {
                    itemOrçamento.Quantidade += 1;
                    Model1.AddQuantidade(itemOrçamento);
                }
                final += peça.Preço;
            }

            if (serviçoPeçaTotal.Total != null)
            {
                decimal valor = Decimal.Parse(serviçoPeçaTotal.Total);
                decimal original = (decimal)Session["ValorOriginal"];


                if (final + original == valor)
                {
                    orçamento.Data_Aprovação = DateTime.Now;
                    orçamento.Valor = valor;

                    Model1.AprovarFinalizarOrçamento(orçamento.Id, 1);
                    return JavaScript($"window.location='/Orçamento/StatusOrçamentoOficina/'" + orçamento.Id);
                }
            }

            return View();

        }

    }
}
