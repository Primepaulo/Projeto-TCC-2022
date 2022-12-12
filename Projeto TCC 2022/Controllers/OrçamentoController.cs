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
        public PartialViewResult VisualizarOrçamentoPessoaPartial(int data)
        {
            List<Orçamento> orçamentos = new List<Orçamento>();

            if (data == 1)
            {
                orçamentos = Model1.GetOrçamentos(UserID);
            }

            else if (data == 2)
            {
               orçamentos = Model1.GetAllOrçamentos(UserID);
            }

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

        public PartialViewResult VisualizarOrçamentoOficinaPartial(int data)
        {
            List<Orçamento> orçamentos = Model1.GetOrçamentos(UserID);

            if (data == 1)
            {
                orçamentos = Model1.GetOrçamentos(UserID);
            }

            else if (data == 2)
            {
                orçamentos = Model1.GetAllOrçamentos(UserID);
            }

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

        public ActionResult HistoricoOrçamentos()
        {
            return View();
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
                Model1.GerarNotificações(orçamento, orçamento.fk_Oficina_Id);

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
                Session["ServiçoOrçamentoMin"] = serviço.PreçoMin;
                Session["ServiçoOrçamentoMax"] = serviço.PreçoMax;
                ViewBag.Tipo = 1;
            }

            else if (Tipo == 2)
            {
                var peça = Model1.GetPeça(Id);
                ViewBag.Peça = peça;
                Session["ServiçoOrçamentoMin"] = peça.PreçoMin;
                Session["ServiçoOrçamentoMax"] = peça.PreçoMax;
                ViewBag.Tipo = 2;
            }

            return PartialView();
        }

        public PartialViewResult TotalPartial()
        {
            decimal somaMin = 0;
            decimal somaMax = 0;

            decimal valorMin = Convert.ToDecimal(Session["ServiçoOrçamentoMin"]);
            decimal valorMax = Convert.ToDecimal(Session["ServiçoOrçamentoMax"]);

            if (Session["SomaMin"] == null && Session["SomaMax"] == null) // Bugs
            {
                somaMin += valorMin;
                somaMax += valorMax;
            }
            else
            {
                somaMin = Convert.ToDecimal(Session["SomaMin"]) + valorMin;
                somaMax = Convert.ToDecimal(Session["SomaMax"]) + valorMax;
            }

            Session["SomaMin"] = somaMin;
            Session["SomaMax"] = somaMax;
            ViewBag.TotalMin = somaMin;
            ViewBag.TotalMax = somaMax;
            return PartialView();
        }

        [HttpGet]
        public ActionResult LimparOrçamento(int? Type)
        {
            if (Type == 2 || Type == null)
            {
                Session["ServiçoOrçamento"] = null;
                Session["SomaMin"] = null;
                Session["SomaMax"] = null;
                ViewBag.TotalMin = null;
                ViewBag.TotalMax = null;
            }
            else if (Type == 1)
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
            decimal finalMin = 0;
            decimal finalMax = 0;

            foreach (string item in serviçoTotal.serviços)
            {
                Serviço serviço = Model1.GetServiço(Convert.ToInt32(item));

                if (serviço.PreçoMin != null)
                {
                    finalMin += (decimal)serviço.PreçoMin;
                }

                if (serviço.PreçoMax != null)
                {
                    finalMax += (decimal)serviço.PreçoMax;
                }

            }
            if (Session["OficinaId"] != null && Session["carro"] != null && serviçoTotal.serviços != null)
            {
                decimal valorMin = Decimal.Parse(serviçoTotal.TotalMin);
                decimal valorMax = Decimal.Parse(serviçoTotal.TotalMax);
                if (finalMin == valorMin && finalMax == valorMax)
                {
                    Carro carro = (Carro)Session["carro"];
                    int oficinaId = (int)Session["OficinaId"];
                    DateTime data = DateTime.Now;

                    Orçamento orçamento = Model1.CreateOrçamento(UserID, carro.Placa, oficinaId, data, 1);
                    Model1.GerarNotificações(orçamento, orçamento.fk_Oficina_Id);

                    foreach (string item in serviçoTotal.serviços)
                    {
                        int ServiçoId = Convert.ToInt32(item);
                        ItemOrçamento itemOrçamento = Model1.GetItemOrçamentoServiço(orçamento.Id, ServiçoId);
                        if (itemOrçamento == null)
                        {
                            Model1.AddItemOrçamento(orçamento.Id, ServiçoId, null, 1, false);
                        }
                        else
                        {
                            itemOrçamento.Quantidade += 1;
                            Model1.AddQuantidade(itemOrçamento);
                        }
                    }
                    return JavaScript($"window.location='/Orçamento/StatusOrçamentoPessoa/" + orçamento.Id + "'");
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

            ViewBag.ItemOrçamento = Model1.GetItems(Id);

            List<Peça> peças = new List<Peça>();
            List<Serviço> serviços = new List<Serviço>();
            List<Avaliação> avaliações = new List<Avaliação>();

            foreach (var item in ViewBag.ItemOrçamento)
            {
                if (item.Fk_Serviço_Id != null) {
                    serviços.Add(Model1.GetServiço(item.Fk_Serviço_Id));
                }
                else if (item.Fk_Peça_Id != null)
                {
                    peças.Add(Model1.GetPeça(item.Fk_Peça_Id));
                }
            }

            ViewBag.Peças = peças;
            ViewBag.Serviços = serviços;

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
                ViewBag.ItemOrçamento = Model1.GetItems(Id);

                List<Peça> peças = new List<Peça>();
                List<Serviço> serviços = new List<Serviço>();
                if (ViewBag.ItemOrçamento != null)
                {
                    foreach (var item in ViewBag.ItemOrçamento)
                    {
                        if (item.Fk_Serviço_Id != null)
                        {
                            serviços.Add(Model1.GetServiço(item.Fk_Serviço_Id));
                        }
                        else if (item.Fk_Peça_Id != null)
                        {
                            peças.Add(Model1.GetPeça(item.Fk_Peça_Id));
                        }
                    }
                }

                ViewBag.Peças = peças;
                ViewBag.Serviços = serviços;

                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ConfirmarDeletarOrçamento(int Id, int Operação)
        {
            Orçamento orçamento = Model1.GetOrçamento(Id);
            if (orçamento.fk_Oficina_Id == UserID && Operação == 1)
            {
                return RedirectToAction("AdicionarPeçaServiço/" + orçamento.Id);

            }
            else if (orçamento.fk_Pessoa_Id == UserID && Operação == 2)
            {
                Model1.AprovarFinalizarOrçamento(orçamento.Id, Operação, null);
                Model1.GerarNotificações(orçamento, orçamento.fk_Oficina_Id);
                return RedirectToAction("StatusOrçamentoPessoa", "Orçamento", new { orçamento.Id });
            }
            else if (orçamento.fk_Oficina_Id == UserID && (Operação == 3 || Operação == 4))
            {
                Model1.AprovarFinalizarOrçamento(orçamento.Id, Operação, null);
                Model1.GerarNotificações(orçamento, orçamento.fk_Pessoa_Id);
                return RedirectToAction("VisualizarOrçamentos");
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

                    //Mudar essa parte toda...
                    Session["SomaMin"] = orçamento.Valor;
                    ViewBag.Total = Session["Soma"];

                    Session["ValorOriginalMin"] = Session["SomaMin"];
                    Session["ValorOriginalMax"] = Session["SomaMax"];
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
                        Model1.AddItemOrçamento(orçamento.Id, serviço.Id, null, 1, false);
                    }
                    else
                    {
                        itemOrçamento.Quantidade += 1;
                        Model1.AddQuantidade(itemOrçamento);
                    }

                }
            }

            if (serviçoPeçaTotal.peças != null)
            {

                foreach (string item in serviçoPeçaTotal.peças)
                {
                    Peça peça = Model1.GetPeça(Convert.ToInt32(item));
                    ItemOrçamento itemOrçamento = Model1.GetItemOrçamentoPeça(orçamento.Id, peça.Id);
                    if (itemOrçamento == null)
                    {
                        Model1.AddItemOrçamento(orçamento.Id, null, peça.Id, 1, null);
                    }
                    else
                    {
                        itemOrçamento.Quantidade += 1;
                        Model1.AddQuantidade(itemOrçamento);
                    }
                    final += peça.Preço;
                }
            }

            if (serviçoPeçaTotal.Total != null)
            {
                decimal valor = Decimal.Parse(serviçoPeçaTotal.Total);
                decimal original;

                if (Session["ValorOriginal"] != null)
                {
                    original = (decimal)Session["ValorOriginal"];
                }

                else 
                { 
                    original = 0;
                }

                if (final + original == valor)
                {
                    orçamento.Valor = valor;

                    Model1.AprovarFinalizarOrçamento(orçamento.Id, 1, valor);
                    Model1.GerarNotificações(orçamento, orçamento.fk_Pessoa_Id);
                    return JavaScript($"window.location='/Orçamento/StatusOrçamentoOficina/" + orçamento.Id + "'");
                }
            }

            else if (serviçoPeçaTotal.Total == null)
            {
                var valor = (decimal)Session["ValorOriginal"];
                Model1.AprovarFinalizarOrçamento(orçamento.Id, 1, valor);
                Model1.GerarNotificações(orçamento, orçamento.fk_Pessoa_Id);
                return JavaScript($"window.location='/Orçamento/StatusOrçamentoOficina/" + orçamento.Id + "'");
            }

            return View();

        }

    }
}
