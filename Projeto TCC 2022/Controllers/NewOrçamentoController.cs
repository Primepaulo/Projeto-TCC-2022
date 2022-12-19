using Projeto_TCC_2022.Models;
using Projeto_TCC_2022.Models.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class NewOrçamentoController : DefaultController
    {
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
                return RedirectToAction("AgendarServiço", "NewOrçamento", new { Id });
            }
            else if (Value == true)
            {
                return RedirectToAction("AgendarAnálise", "NewOrçamento", new { Id });
            }
            else
                return RedirectToAction("Erro"); //TBA
        }

        //Fase 1-1:

        public ActionResult AgendarServiço(int Id)
        {
            Session["ServiçoOrçamentoMin"] = null;
            Session["ServiçoOrçamentoMax"] = null;
            Session["SomaMin"] = null;
            Session["SomaMax"] = null;
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


        public PartialViewResult ItemOrçamentoPartial1(int Id, string Tipo)
        {
            ServiçoCategoria serviçoCategoria = new ServiçoCategoria();
            Serviço serviço = Model1.GetServiço(Id);
            Categoria categoria = Model1.GetCategoriaById(serviço.Fk_Categoria_Id);

            serviçoCategoria.Serviço = serviço;
            serviçoCategoria.Categoria = categoria;

            ViewBag.Tipo = Tipo;

            if (Tipo == "")
            {
                Session["ServiçoOrçamentoMin"] = serviço.PreçoMin;
                Session["ServiçoOrçamentoMax"] = serviço.PreçoMax;
            }

            return PartialView(serviçoCategoria);
        }

        public PartialViewResult TotalPartial1()
        {
            decimal somaMax = 0;
            decimal somaMin = 0;


            decimal valorMin = Convert.ToDecimal(Session["ServiçoOrçamentoMin"]);
            decimal valorMax = Convert.ToDecimal(Session["ServiçoOrçamentoMax"]);

            if (Session["SomaMin"] == null && Session["SomaMax"] == null)
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
        public ActionResult LimparOrçamento1()
        {
            Session["ServiçoOrçamento"] = null;
            Session["SomaMin"] = null;
            Session["SomaMax"] = null;
            ViewBag.TotalMin = null;
            ViewBag.TotalMax = null;
            return null;
        }

        [HttpPost]
        public ActionResult Submit1(ServiçoTotal serviçoTotal)
        {
            if (Session["OficinaId"] != null && Session["carro"] != null && serviçoTotal.serviços != null)
            {
                Carro carro = (Carro)Session["carro"];
                int oficinaId = (int)Session["OficinaId"];
                DateTime data = DateTime.Now;

                Orçamento orçamento = Model1.CreateOrçamento(UserID, carro.Placa, oficinaId, data, 1);
                Model1.GerarNotificações(orçamento, orçamento.fk_Oficina_Id);

                for (int i = 0; i < serviçoTotal.serviços.Length; i++)
                {
                    int ServiçoId = Convert.ToInt32(serviçoTotal.serviços[i]);
                    Serviço serviço = Model1.GetServiço(ServiçoId);
                    ItemOrçamento itemOrçamento = Model1.GetItemOrçamentoServiço(orçamento.Id, ServiçoId);
                    if (itemOrçamento == null)
                    {
                        Model1.AddItemOrçamento(orçamento.Id, serviço.Nome, null, serviço.Descrição, 1, false);
                    }
                    else
                    {
                        itemOrçamento.Quantidade += 1;
                        Model1.AddQuantidade(itemOrçamento);
                    }
                }
                    return JavaScript($"window.location='/NewOrçamento/StatusOrçamento/" + orçamento.Id + "'");
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

        public ActionResult AgendarAnálise()
        {
            Carro carro = (Carro)Session["carro"];
            int oficinaId = (int)Session["OficinaId"];
            DateTime data = DateTime.Now;
            Orçamento orçamento = Model1.CreateOrçamento(UserID, carro.Placa, oficinaId, data, 2);
            Model1.GerarNotificações(orçamento, orçamento.fk_Oficina_Id);
            return RedirectToAction("StatusOrçamento", "NewOrçamento", new { orçamento.Id });
        }

        //Fase x:

        public ActionResult StatusOrçamento(int Id)
        {
            Response.Clear();
            Session.Clear();
            Orçamento orçamento = Model1.GetOrçamento(Id);

            if (orçamento.fk_Pessoa_Id == UserID || orçamento.fk_Oficina_Id == UserID)
            {
                return View(orçamento);
            }
            return RedirectToAction("Index", "Home");
        }

        //Fase 1-x:

        public ActionResult OficinaApprovePartial(int Id)
        {
            List<ItemOrçamento> itens = Model1.GetItems(Id);
            Orçamento orçamento = Model1.GetOrçamento(Id);
            if (orçamento.fk_Oficina_Id == UserID || orçamento.fk_Pessoa_Id == UserID)
            {
                bool Marcado = false;
                foreach (var item in itens)
                {
                    if (orçamento.Tipo == 1 && Model1.GetServiçoByNomeId(orçamento.fk_Oficina_Id, item.Nome).NecessitaAvaliarVeiculo == true)
                    {
                        Marcado = true;
                    }
                }

                if (orçamento.Tipo == 2)
                {
                    Marcado = true;
                }

                OficinaApprovePartial oficinaApprovePartial = new OficinaApprovePartial
                {
                    Marcar = Marcado,
                    Id = Id
                };

                return PartialView(oficinaApprovePartial);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult AprovarRecusar(int Id, int Operação, string fim, string inicio)
        {
            // Operação é 12(aprovado) ou 11(recusado)
            string HorarioFuncionamento = null;
            Debug.WriteLine(Id);
            if (fim != null && inicio != null)
            {
                HorarioFuncionamento = inicio + "/" + fim;
            }
            Orçamento orçamento = Model1.GetOrçamento(Id);
            if (orçamento.fk_Oficina_Id == UserID)
            {
                if (HorarioFuncionamento == null || Operação == 11)
                {
                    Model1.AprovarFinalizarOrçamento(Id, Operação, null, null);
                }

                if (HorarioFuncionamento != null && Operação == 12)
                {
                    Operação = 21; //Aguardando Avaliação do Veículo;
                    Model1.AprovarFinalizarOrçamento(Id, Operação, null, HorarioFuncionamento);
                }

                return RedirectToAction("StatusOrçamento", "NewOrçamento", new { Id });
            }

            return RedirectToAction("Index", "Home");
        }

        //Fase 2-x:
        public ActionResult ConfirmarAvaliaçãoPartial(int Id)
        {
            ViewBag.Id = Id;
            return PartialView();
        }
        [HttpPost]
        public ActionResult ConfirmarAvaliação(int Id, int Operação)
        {
            Orçamento orçamento = Model1.GetOrçamento(Id);
            if (Operação == 22)
            {
                orçamento.Status = 22;
                Model1.UpdateOrçamento(orçamento);
            }
            if (Operação == 24)
            {
                orçamento.Status = 24;
                Model1.UpdateOrçamento(orçamento);
            }
            return RedirectToAction("StatusOrçamento", "NewOrçamento", new {Id});
        }
        public ActionResult FormularPromptPartial(int Id)
        {
            Orçamento orçamento = Model1.GetOrçamento(Id);
            ViewBag.Id = Id;
            if (orçamento.Status == 22)
            {
                ViewBag.Formulação = true;
            }
            return PartialView();
        }
        [HttpPost]
        public ActionResult Formulando(int Id)
        {
            Orçamento orçamento = Model1.GetOrçamento(Id);
            orçamento.Status = 22;
            Model1.UpdateOrçamento(orçamento);
            return RedirectToAction("AddItemOrçamento2", "NewOrçamento", new { Id });
        }

        public ActionResult AddItemOrçamento2(int Id)
        {
            Orçamento orçamento = Model1.GetOrçamento(Id);
            if (orçamento.fk_Oficina_Id == UserID)
            {
                List<Serviço> Inseridos = new List<Serviço>();
                List<ItemOrçamento> itens = Model1.GetItems(orçamento.fk_Oficina_Id);
                List<Serviço> serviços = Model1.GetServiços(orçamento.fk_Oficina_Id);

                foreach (var item in itens)
                {
                    Inseridos.Add(Model1.GetServiçoByNomeId(UserID, item.Nome));
                }


                List<Categoria> categorias = new List<Categoria>();
                List<Peça> peças = Model1.GetPeças(orçamento.fk_Oficina_Id);

                foreach (Serviço serviço in serviços)
                {
                    Categoria categoria = Model1.GetCategoriaById(serviço.Fk_Categoria_Id);
                    if (!categorias.Contains(categoria))
                    {
                        categorias.Add(categoria);
                    }
                }

                AddItemOrçamento2View ViewModel = new AddItemOrçamento2View
                {
                    Itens = itens,
                    Oficina = Model1.GetOficinaById(orçamento.fk_Oficina_Id),
                    Categorias = categorias,
                    Peças = peças,
                    Padrão = Inseridos
                };

                return View(ViewModel);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ItemOrçamentoPartial2(int Id, string Tipo)
        {
            Debug.WriteLine(Tipo);
            if (Tipo == "Serviço")
            {
                return RedirectToAction("ItemOrçamentoPartial1", "NewOrçamento", new { Id, Tipo });
            }

            if (Tipo == "Peça")
            {
                Peça peça = Model1.GetPeça(Id);
                if (peça.Fk_Oficina_Id == UserID)
                {
                    return PartialView(peça);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult TotalPartial2(PeçaServiço itens)
        {
            decimal IMin = 0;
            decimal IMax = 0;
            decimal total = 0;

            if (itens.Peças != null)
            {
                foreach (var item in itens.Peças)
                {
                    Peça peça = Model1.GetPeça(Convert.ToInt32(item));
                    if (peça.PreçoMin == null)
                    {
                        peça.PreçoMin = 0;
                    }
                    if (peça.PreçoMax == null)
                    {
                        peça.PreçoMax = 0;
                    }

                    IMin += (decimal)peça.PreçoMin;
                    IMax += (decimal)peça.PreçoMax;
                }
            }

            if (itens.serviços != null)
            {
                foreach (var item in itens.serviços)
                {
                    Serviço serviço = Model1.GetServiço(Convert.ToInt32(item));
                    if (serviço.PreçoMin == null)
                    {
                        serviço.PreçoMin = 0;
                    }
                    if (serviço.PreçoMax == null)
                    {
                        serviço.PreçoMax = 0;
                    }

                    IMin += (decimal)serviço.PreçoMin;
                    IMax += (decimal)serviço.PreçoMax;
                }
            }

            if (itens.final != null)
            {
                foreach (string item in itens.final)
                {
                    total += Decimal.Parse(item);
                }
            }

            Session["ValorIMin"] = IMin;
            Session["ValorIMax"] = IMax;

            ViewBag.TotalMin = IMin;
            ViewBag.TotalMax = IMax;
            ViewBag.Total = total;

            return PartialView();
        }

        [HttpGet]
        public ActionResult LimparOrçamento2()
        {
            Session["ServiçoOrçamento"] = null;
            ViewBag.Total = null;
            return null;
        }

        [HttpPost]
        public ActionResult FormularOrçamento(ListItemViewModel ItensOrçamento)
        {
            int OrçamentoId = ItensOrçamento.OrçamentoId;
            
            foreach (ItemViewModel item in ItensOrçamento.Items)
            {
                if (item.Tipo == 1)
                {
                    Serviço serviço = Model1.GetServiço(item.Id);
                    ItemOrçamento itemOrçamento = Model1.GetItemOrçamentoServiço(OrçamentoId, item.Id);
                    if (itemOrçamento == null)
                    {
                        Model1.AddItemOrçamento(OrçamentoId, serviço.Nome, item.Preço, serviço.Descrição, 1, false);
                    }
                    else
                    {
                        itemOrçamento.Quantidade += 1;
                        Model1.AddQuantidade(itemOrçamento);
                        Model1.AdicionarPreço(itemOrçamento, item.Preço);
                    }
                }

                if (item.Tipo == 2)
                {
                    Peça peça = Model1.GetPeça(item.Id);
                    ItemOrçamento itemOrçamento = Model1.GetItemOrçamentoPeça(OrçamentoId, item.Id);
                    if (itemOrçamento == null)
                    {
                        Model1.AddItemOrçamento(OrçamentoId, peça.Nome, item.Preço, peça.Descrição, 1, null);
                    }
                    else
                    {
                        itemOrçamento.Quantidade += 1;
                        Model1.AddQuantidade(itemOrçamento);
                        if (itemOrçamento.Preço == null)
                        {
                            Model1.AdicionarPreço(itemOrçamento, item.Preço);
                        }
                    }
                }
                Model1.AprovarFinalizarOrçamento(OrçamentoId, 23, Decimal.Parse(ItensOrçamento.Total), null);
            }
            return JavaScript($"window.location='/NewOrçamento/StatusOrçamento/" + OrçamentoId + "'");
        }












        public ActionResult HistoricoOrçamentos()
        {
            return View();
        }

    }
}