using Projeto_TCC_2022.Models;
using Projeto_TCC_2022.Models.Classes;
using System;
using System.Collections.Generic;
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
                return RedirectToAction("AgendarServiço", "Orçamento", new { Id });
            }
            else if (Value == true)
            {
                return RedirectToAction("AgendarAnálise", "Orçamento", new { Id });
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


        public PartialViewResult ItemOrçamentoPartial1(int Id)
        {
            ServiçoCategoria serviçoCategoria = new ServiçoCategoria();
            Serviço serviço = Model1.GetServiço(Id);
            Categoria categoria = Model1.GetCategoriaById(serviço.Fk_Categoria_Id);

            serviçoCategoria.Serviço = serviço;
            serviçoCategoria.Categoria = categoria;

            ViewBag.ServiçoCategoria = serviçoCategoria;
            Session["ServiçoOrçamentoMin"] = serviço.PreçoMin;
            Session["ServiçoOrçamentoMax"] = serviço.PreçoMax;

            return PartialView();
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

            Session["Soma10Min"] = somaMin;
            Session["Soma10Max"] = somaMax;
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
                decimal valorMin = Decimal.Parse(serviçoTotal.TotalMin);
                decimal valorMax = Decimal.Parse(serviçoTotal.TotalMax);
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

            /*Colocar na view:
             
            if (orçamento.Status == Recusado) e if (orçamento.Status == Finalizado) e  if (orçamento.Status == Abandonado)

            não loadar a partial view e colocar o texto direto.
             
            */

            return RedirectToAction("Index", "Home");
        }

        //Fase 1-x:

        public ActionResult OficinaApprovePartial(int Id)
        {
            return PartialView(Id);
        }

        [HttpPost]
        public ActionResult AprovarRecusar(int Id, int Operação, string DateTime)
        {
            // Operação é 12(aprovado) ou 11(recusado)

            Orçamento orçamento = Model1.GetOrçamento(Id);
            if (orçamento.fk_Oficina_Id == UserID)
            {
                if (DateTime == null)
                {
                    Model1.AprovarFinalizarOrçamento(Id, Operação, null, null);
                }

                if (DateTime != null)
                {
                    Model1.AprovarFinalizarOrçamento(Id, Operação, null, DateTime);
                }

                return RedirectToAction("StatusOrçamento", "Orçamento", Id);
            }

            return RedirectToAction("Index", "Home");
        }

        //Fase 2-x:
        public ActionResult FormularPromptPartial(int Id)
        {
            return PartialView(Id);
        }

        public ActionResult AddItemOrçamento2(int Id)
        {
            List<ItemOrçamento> itens = Model1.GetItems(Id);
            List<Serviço> serviços = Model1.GetServiços(Id);
            List<Categoria> categorias = new List<Categoria>();
            List<Peça> peças = Model1.GetPeças(Id);

            foreach (Serviço serviço in serviços)
            {
                Categoria categoria = Model1.GetCategoriaById(serviço.Fk_Categoria_Id);
                if (!categorias.Contains(categoria))
                {
                    categorias.Add(categoria);
                }
            }

            ViewBag.Serviços = serviços;
            ViewBag.ServiçoCategoria = categorias;
            ViewBag.Peças = peças;
            ViewBag.Itens = itens;

            return View();
        }

        public ActionResult ItemOrçamentoPartial2(int Id, string Tipo)
        {
            if (Tipo == "Serviço")
            {
                ServiçoCategoria serviçoCategoria = new ServiçoCategoria();
                Serviço serviço = Model1.GetServiço(Id);
                Categoria categoria = Model1.GetCategoriaById(serviço.Fk_Categoria_Id);

                serviçoCategoria.Serviço = serviço;
                serviçoCategoria.Categoria = categoria;

                return PartialView(serviçoCategoria);
            }

            if (Tipo == "Peça")
            {
                Peça peça = Model1.GetPeça(Id);
                return PartialView(peça);
            }

            return PartialView();
        }

        public ActionResult TotalPartial2(ValorMinMax[] itens, string[] final)
        {
            decimal IMin = 0;
            decimal IMax = 0;
            decimal total = 0;

            foreach (ValorMinMax item in itens)
            {
                IMin += item.Min;
                IMax += item.Max;
            }

            foreach (string item in final)
            {
                total += Decimal.Parse(item);
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
                Model1.AprovarFinalizarOrçamento(OrçamentoId, 23, ItensOrçamento.Total, null);
            }
            return JavaScript($"window.location='/NewOrçamento/StatusOrçamento/" + OrçamentoId + "'");
        }












        public ActionResult HistoricoOrçamentos()
        {
            return View();
        }

    }
}