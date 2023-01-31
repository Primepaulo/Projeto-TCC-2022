using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using Projeto_TCC_2022.Models;
using Projeto_TCC_2022.Models.Classes;
using Projeto_TCC_2022.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class OrçamentoController : DefaultController
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
            return RedirectToAction("Marcar", "Orçamento", new { Id, Value });
        }

        //Fase 0-2:
        public ActionResult Marcar(int Id, bool Value, int? Skip)
        {
            Oficina oficina = Model1.GetOficinaById(Id);
            List<Agendamento> agendamentos = Model1.GetAgendamentos(Id);

            Calendario calendario = new Calendario
            {
                Oficina = oficina,
                Agendamentos = agendamentos,
                Value = Value,
                Skip = Skip
            };

            return View(calendario);
        }

        public ActionResult MarcarModalPartial(int Id, string Dia, string Month)
        {
            Oficina oficina = Model1.GetOficinaById(Id);
            List<Agendamento> agendamentos = Model1.GetAgendamentos(Id);
            List<Agendamento> Datas = new List<Agendamento>();
            var x = DateTime.Today;

            int intDia = Convert.ToInt32(Dia);
            int intMonth = Convert.ToInt32(Month);

            Datas = agendamentos
                    .Where(e => e.Data.Day == intDia && e.Data.Month == intMonth && e.Data.Year == x.Year).ToList();

            Calendario calendario = new Calendario
            {
                Agendamentos = Datas,
                Oficina = oficina,
                Dia = intDia
                
            };

            return PartialView(calendario);
        }

        [HttpPost]
        public ActionResult EscolherHorario(bool Value, string Date, string Time, int Id, int? Skip)
        {
            var Actual = DateTime.Today.AddDays(Convert.ToInt32(Date));
            var ActualDate = Actual.Date.ToString().Split(' ')[0];

            DateTime Data = DateTime.Parse(ActualDate + " " + Time);

            if (Model1.GetAgendamentoByHora(Data) == null)
            {
                if (Skip != null)
                {
                    Orçamento orçamento = Model1.GetOrçamento((int)Skip);
                    Agendamento agendamento = Model1.GetAgendamentoByOrçamento(orçamento.Id);
                    agendamento.Data = Data;
                    agendamento.Finalizado = false;
                    Model1.UpdateAgendamento(agendamento);
                    Model1.AprovarFinalizarOrçamento((int)Skip, 40, null);
                    Model1.GerarNotificações(orçamento, orçamento.fk_Oficina_Id, "Orçamento Remarcado!");
                    return RedirectToAction("StatusOrçamento", "Orçamento", new { Id = (int)Skip });
                }

                Session["Agendamento"] = Model1.Agendar(Data, Id, UserID);



                if (Value == false)
                {
                    return RedirectToAction("AgendarServiço", "Orçamento", new { Id });
                }
                else if (Value == true)
                {
                    return RedirectToAction("AgendarAnálise", "Orçamento", new { Id });
                }
            }
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


        public PartialViewResult ItemOrçamentoPartial1(int Id, string Tipo, int? Quant)
        {
            ServiçoCategoria serviçoCategoria = new ServiçoCategoria();
            Serviço serviço = Model1.GetServiço(Id);
            Categoria categoria = Model1.GetCategoriaById(serviço.Fk_Categoria_Id);

            serviçoCategoria.Serviço = serviço;
            serviçoCategoria.Categoria = categoria;
            serviçoCategoria.Quant = Quant;

            ViewBag.Tipo = Tipo;

            if (Tipo == null)
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
                Model1.GerarNotificações(orçamento, orçamento.fk_Oficina_Id, "Nova solicitação de Orçamento!");
                Agendamento agendamento = (Agendamento)Session["Agendamento"];
                agendamento.Fk_Orçamento_Id = orçamento.Id;
                Model1.UpdateAgendamento(agendamento);

                for (int i = 0; i < serviçoTotal.serviços.Length; i++)
                {
                    int ServiçoId = Convert.ToInt32(serviçoTotal.serviços[i]);
                    Serviço serviço = Model1.GetServiço(ServiçoId);
                    Model1.AddItemOrçamento(orçamento.Id, serviço.Nome, null, serviço.Descrição, 1, false, 1);
                }
                return JavaScript($"window.location='/Orçamento/StatusOrçamento/" + orçamento.Id + "'");
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
            Model1.GerarNotificações(orçamento, orçamento.fk_Oficina_Id, "Nova solicitação de Orçamento!");
            Agendamento agendamento = (Agendamento)Session["Agendamento"];
            agendamento.Fk_Orçamento_Id = orçamento.Id;
            Model1.UpdateAgendamento(agendamento);
            return RedirectToAction("StatusOrçamento", "Orçamento", new { orçamento.Id });
        }

        //Fase x:

        public ActionResult StatusOrçamento(int Id)
        {
            Response.Clear();
            Session.Clear();
            Orçamento orçamento = Model1.GetOrçamento(Id);

            if (orçamento != null)
            {
                if (orçamento.fk_Pessoa_Id == UserID || orçamento.fk_Oficina_Id == UserID)
                {
                    return View(orçamento);
                }
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

                OficinaApprovePartial oficinaApprovePartial = new OficinaApprovePartial
                {
                    Orçamento = orçamento,
                    Id = Id
                };

                return PartialView(oficinaApprovePartial);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult AprovarRecusar(int Id, int Operação)
        {

            Orçamento orçamento = Model1.GetOrçamento(Id);
            if (orçamento.fk_Oficina_Id == UserID)
            {
                if (Operação == 11)
                {
                    Model1.AprovarFinalizarOrçamento(Id, Operação, null);
                    Model1.GerarNotificações(orçamento, orçamento.fk_Pessoa_Id, "Solicitação de orçamento recusada pela oficina.");
                    Model1.FinalizarAgendamento(Id);
                }

                else if (Operação == 12)
                {
                    Operação = 40;
                    Model1.AprovarFinalizarOrçamento(Id, Operação, null);
                    Model1.GerarNotificações(orçamento, orçamento.fk_Pessoa_Id, "Solicitação de orçamento aprovada pela oficina!");
                }

                return RedirectToAction("StatusOrçamento", "Orçamento", new { Id });
            }

            return RedirectToAction("Index", "Home");
        }

        //Fase 1.5-x:

        public ActionResult ConfirmarAgendamentoPartial(int OrçamentoId)
        {
            Orçamento orçamento = Model1.GetOrçamento(OrçamentoId);
            Agendamento agendamento = Model1.GetAgendamentoByOrçamento(OrçamentoId);
            ConfirmarAgendamentoPartialModel model = new ConfirmarAgendamentoPartialModel
            {
                Orçamento = orçamento,
                Agendamento = agendamento
            };

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult AgendamentoPOST(ConfirmarAgendamentoPartialModel model, int Operação)
        {
            Orçamento orçamento = Model1.GetOrçamento(model.Orçamento.Id);
            if (orçamento.fk_Pessoa_Id == UserID || orçamento.fk_Oficina_Id == UserID)
            {
                if (Operação == 13)
                {
                    bool val;
                    if (orçamento.Tipo == 1)
                    {
                        val = false;
                    }
                    else
                        val = true;
                    return RedirectToAction("Marcar", "Orçamento", new { Id = orçamento.fk_Oficina_Id, Value = val, Skip = orçamento.Id });
                }

                if (Operação == 41)
                {
                    Model1.AprovarFinalizarOrçamento(orçamento.Id, 13, null);
                    Model1.FinalizarAgendamento(orçamento.Id);
                    Model1.GerarNotificações(orçamento, orçamento.fk_Pessoa_Id, "Agendamento Recusado. Remarcar?");
                }
                if (Operação == 42)
                {
                    Model1.AprovarFinalizarOrçamento(orçamento.Id, 21, null);
                    Model1.GerarNotificações(orçamento, orçamento.fk_Pessoa_Id, 
                        "Agendamento Aprovado. Leve o veículo até a Oficina na data agendada");
                }
                if (Operação == 43)
                {
                    Model1.AprovarFinalizarOrçamento(orçamento.Id, Operação, null);
                    Model1.FinalizarAgendamento(orçamento.Id);
                    Model1.GerarNotificações(orçamento, orçamento.fk_Pessoa_Id,"Orçamento Cancelado.");
                    Model1.GerarNotificações(orçamento, orçamento.fk_Oficina_Id, "Orçamento Cancelado.");
                }
            }

            return RedirectToAction("StatusOrçamento", "Orçamento", new { Id = orçamento.Id });
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
                Model1.FinalizarAgendamento(orçamento.Id);
                Model1.GerarNotificações(orçamento, orçamento.fk_Pessoa_Id, "Avaliação do Veículo confirmada!");
            }
            if (Operação == 24)
            {
                orçamento.Status = 24;
                Model1.UpdateOrçamento(orçamento);
                Model1.FinalizarAgendamento(orçamento.Id);
                Model1.GerarNotificações(orçamento, orçamento.fk_Pessoa_Id, "Veículo não foi levado para análise.");
            }

            return RedirectToAction("StatusOrçamento", "Orçamento", new { Id });
        }

        [HttpPost]
        public ActionResult Cancelar(int Id)
        {
            Orçamento orçamento = Model1.GetOrçamento(Id);

            if (orçamento.fk_Pessoa_Id == UserID)
            {
                orçamento.Status = 26;
                Model1.UpdateOrçamento(orçamento);
            }

            if (orçamento.fk_Oficina_Id == UserID)
            {
                orçamento.Status = 25;
                Model1.UpdateOrçamento(orçamento);
            }

            return RedirectToAction("StatusOrçamento", "Orçamento", new { Id });
        }

        public ActionResult FormularPromptPartial(int Id)
        {
            Orçamento orçamento = Model1.GetOrçamento(Id);
            if (orçamento.Status == 22)
            {
                ViewBag.Formulação = true;
            }
            return PartialView(orçamento);
        }
        [HttpPost]
        public ActionResult Formulando(int Id)
        {
            Orçamento orçamento = Model1.GetOrçamento(Id);
            orçamento.Status = 22;
            Model1.UpdateOrçamento(orçamento);
            Model1.GerarNotificações(orçamento, orçamento.fk_Pessoa_Id, "A Oficina já está trabalhando no seu orçamento!");
            return RedirectToAction("AddItemOrçamento2", "Orçamento", new { Id });
        }

        public ActionResult AddItemOrçamento2(int Id)
        {
            Orçamento orçamento = Model1.GetOrçamento(Id);
            if (orçamento.fk_Oficina_Id == UserID)
            {
                List<Serviço> Inseridos = new List<Serviço>();
                List<ItemOrçamento> itens = Model1.GetItems(orçamento.Id);
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
                    OrçamentoId = Id,
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

        public ActionResult ItemOrçamentoPartial2(int Id, string Tipo, int Quant)
        {
            if (Tipo == "Serviço")
            {
                return RedirectToAction("ItemOrçamentoPartial1", "Orçamento", new { Id, Tipo, Quant });
            }

            if (Tipo == "Peça")
            {
                Peça peça = Model1.GetPeça(Id);
                PeçaQuant peçaQuant = new PeçaQuant
                {
                    Peça = peça,
                    Quant = Quant
                };

                if (peça.Fk_Oficina_Id == UserID)
                {
                    return PartialView(peçaQuant);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
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

            foreach (string item in itens.final)
            {
                if (item.IsNullOrWhiteSpace() != true)
                {
                    string nItem = item.Replace(".", ",");
                    total += Decimal.Parse(nItem);
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

            foreach (ItemViewModel item in ItensOrçamento.Itens)
            {
                if (item.Finalizado != true)
                {
                    if (item.Val.Contains(".") == true)
                    {
                        item.Val = item.Val.Replace(".", ",");
                    }

                    if (Decimal.TryParse(item.Val, out decimal x) == true)
                    {

                        if (item.Tipo == 1)
                        {
                            Serviço serviço = Model1.GetServiço(item.Id);
                            Model1.AddItemOrçamento(OrçamentoId, serviço.Nome, x, serviço.Descrição, 1, false, 1);
                        }

                        else if (item.Tipo == 2)
                        {
                            Peça peça = Model1.GetPeça(item.Id);
                            Model1.AddItemOrçamento(OrçamentoId, peça.Nome, x, peça.Descrição, 1, null, 2);
                        }
                    }
                }

                if (item.Finalizado == true)
                {
                    Model1.RemoveItens(OrçamentoId);
                }
            }

            Orçamento orçamento = Model1.GetOrçamento(OrçamentoId);

            Model1.GerarNotificações(orçamento, orçamento.fk_Pessoa_Id, "Orçamento finalizado, agora só falta aprovar!");
            Model1.AprovarFinalizarOrçamento(OrçamentoId, 23, Decimal.Parse(ItensOrçamento.Final));

            return RedirectToAction("StatusOrçamento", "Orçamento", new { Id = OrçamentoId });
        }

        [HttpPost]
        public ActionResult AprovarOrçamento(int Id, int Operação)
        {
            Orçamento orçamento = Model1.GetOrçamento(Id);

            if (orçamento.fk_Pessoa_Id == UserID)
            {
                Model1.AprovarFinalizarOrçamento(Id, Operação, null);

                if (Operação == 32)
                {
                    Model1.GerarNotificações(orçamento, orçamento.fk_Oficina_Id, "Cliente aprovou o Orçamento!");
                }

                else if (Operação == 31)
                {
                    Model1.GerarNotificações(orçamento, orçamento.fk_Oficina_Id, "Cliente recusou o Orçamento.");
                }
                
            }

            return RedirectToAction("StatusOrçamento", "Orçamento", new { Id });
        }

        public ActionResult HistoricoOrçamentos()
        {
            List<Orçamento> orçamentos = Model1.GetAllOrçamentos(UserID);
            List<Carro> carros = new List<Carro>();
            List<Oficina> oficinas = new List<Oficina>();
            List<Imagem> imagens = new List<Imagem>();
            List<Agendamento> agendamentos = new List<Agendamento>();


            foreach (var orçamento in orçamentos)
            {
                carros.Add(Model1.GetCarro(orçamento.fk_Carro_Placa));
                oficinas.Add(Model1.GetOficinaById(orçamento.fk_Oficina_Id));
                imagens.Add(Model1.GetImagem(orçamento.fk_Oficina_Id));
                agendamentos.Add(Model1.GetAgendamentoByOrçamento(orçamento.Id));
            }

            HistoricoOrçamento historicoOrçamento = new HistoricoOrçamento
            {
                Orçamentos = orçamentos,
                Carros = carros,
                Oficinas = oficinas,
                Imagens = imagens,
                Agendamentos = agendamentos
            };
            return View(historicoOrçamento);
        }

        public ActionResult DetalhesOrçamentoPartial(int Id)
        {
            DetalhesOrçamento detalhes = new DetalhesOrçamento();
            Orçamento orçamento = Model1.GetOrçamento(Id);

            if (orçamento.fk_Oficina_Id == UserID || orçamento.fk_Pessoa_Id == UserID)
            {
                Carro carro = Model1.GetCarro(orçamento.fk_Carro_Placa);
                Oficina oficina = Model1.GetOficinaById(orçamento.fk_Oficina_Id);
                List<ItemOrçamento> itens = Model1.GetItems(Id);
                Imagem imagem = Model1.GetImagem(orçamento.fk_Oficina_Id);
                List<Peça> peças = new List<Peça>();
                List<Serviço> serviços = new List<Serviço>();
                Agendamento agendamento = Model1.GetAgendamentoByOrçamento(orçamento.Id);
                Pessoa pessoa = Model1.GetPessoa(orçamento.fk_Pessoa_Id);
                List<CelularTelefone> Num1 = Model1.GetCelularTelefones(oficina.Id);
                List<CelularTelefone> Num2 = Model1.GetCelularTelefones(pessoa.Id);
                foreach (ItemOrçamento item in itens)
                {
                    Serviço serviço = Model1.GetServiçoByNomeId(oficina.Id, item.Nome);
                    Peça peça = Model1.GetPeçaByUIDNome(oficina.Id, item.Nome);

                    if (peça != null)
                    {
                        peças.Add(peça);
                    }

                    else if (serviço != null)
                    {
                        serviços.Add(serviço);
                    }
                }
                detalhes.Serviços = serviços;
                detalhes.Peças = peças;
                detalhes.Orçamento = orçamento;
                detalhes.ItensOrçamento = itens;
                detalhes.Oficina = oficina;
                detalhes.Carro = carro;
                detalhes.Imagem = imagem;
                detalhes.Agendamento = agendamento;
                detalhes.Pessoa = pessoa;
                detalhes.CelOficina = Num1;
                detalhes.CelPessoa = Num2;

                return PartialView(detalhes);
            }
            return new HttpStatusCodeResult(500);
        }

        //Fim da parte complicada, fique agora com uma frase "motivacional". - Paulo

        //“when I wrote that passage, God and I knew what it meant.
        //It is possible that God knows it still; but as for me,
        //I have totally forgotten.” - John Paul Richter (1826)

    }
}