﻿using Projeto_TCC_2022.Models;
using Projeto_TCC_2022.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class OficinaController : CadController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.éPessoa = Pessoa;
            ViewBag.éOficina = Oficina;
            ViewBag.éAdmin = Admin;
            ViewBag.éAprovada = Aprovada;
            ViewBag.userID = UserID;
            ViewBag.Categorias = Categorias;
            base.OnActionExecuting(filterContext);
        }

        public ActionResult Page(int Id)
        {
            Oficina oficina = Model1.GetOficinaById(Id);
            Page page = new Page();
            if (oficina == null)
            {
                return RedirectToAction("Index", "Home");

            }
            page.Oficina = oficina;
            page.Imagem = Model1.GetImagem(Id);
            page.Cel = Model1.GetCelularTelefones(Id);
            page.Email = Model1.GetEmail(Id);
            page.Serviços = Model1.GetServiços(Id);
            List<AvItemMédia> Itens = new List<AvItemMédia>();
            double MédiaGeral = 0;

            if (page.Serviços != null) {

                List<Avaliação> Av = Model1.GetAvaliaçãoByOficinaId(Id);

                if (Av != null)
                {
                    foreach (var item in Av)
                    {
                        MédiaGeral += item.Estrelas; 
                    }

                    MédiaGeral /= Av.Count();
                }

                foreach (var item in page.Serviços)
                {
                    List<ItemAvaliação> AvItem = Model1.GetItemAvaliaçãoByServiçoId(item.Id);

                    if (AvItem != null)
                    {
                        double média = 0;
                        foreach (var item2 in AvItem)
                        { 
                            média += item2.Estrelas;
                        }

                        média /= AvItem.Count();

                        AvItemMédia avItemMédia = new AvItemMédia
                        {
                            Média = média,
                            Fk_Serviço_Id = item.Id
                        };

                        Itens.Add(avItemMédia);
                    }
                }
            }

            page.Média_Geral = MédiaGeral;
            page.Av = Itens;

            return View(page);
        }

        public ActionResult EditarOficina(int Id)
        {
            if (UserID == Id)
            {
                Oficina oficina = Model1.GetOficinaById(Id);

                string[] itens = oficina.DiasFuncionamento.Split(',');

                Dias dias = Convert(itens);

                Imagem imagem = Model1.GetImagem(Id);
                ViewBag.Imagem = imagem;

                Cadastro cad = new Cadastro
                {
                    Oficina = oficina,
                    Dias = dias
                };

                return View(cad);
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarOficina(Cadastro cadastro, string inicio, string fim)
        {
            if (UserID == cadastro.Oficina.Id && Oficina == true)
            {
                if (ModelState.IsValid)
                {
                    string HorarioFuncionamento = inicio + "/" + fim;
                    cadastro.Oficina.HorarioFuncionamento = HorarioFuncionamento;
                    string DiasFuncionamento = Add(cadastro.Dias);
                    cadastro.Oficina.DiasFuncionamento = DiasFuncionamento;
                    Model1.UpdateOficina(cadastro.Oficina);
                    return RedirectToAction("Page/" + cadastro.Oficina.Id);
                }
            }
            return View(cadastro);
        }

        public ActionResult Sugerir()
        {
            if (Oficina == true && Aprovada == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessage(string Texto)
        {
            if (Oficina == true && Aprovada == true)
            {
                Model1.SendMessage(UserID, Texto, false);
                return RedirectToAction("Sugerir");
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Agendamentos()
        {
            Oficina oficina = Model1.GetOficinaById(UserID);
            DateTime Hoje = DateTime.Today;
            var range = new List<DateTime>();

            for (int i = 0; i < 30; i++)
            {
                range.Add(Hoje.AddDays(i));
            }

            List<Agendamento> agendamentos = Model1.GetAgendamentos(UserID);

            var result = from Agendamento in agendamentos
                         join Data in range
                         on Agendamento.Data.Date equals Data.Date
                         select Agendamento;

            var x = result.ToList();

            Calendario calendario = new Calendario
            {
                Oficina = oficina,
                Agendamentos = x
            };

            return View(calendario);
        }

        public ActionResult AgendamentosModalPartial(string Date)
        {
            DateTime x = DateTime.Parse(Date);
            CalendarioOficina calendario = new CalendarioOficina();

            List<Agendamento> agendamentos = Model1.GetAgendamentoByDate(x);
            if (agendamentos.Count > 0)
            {
                List<Orçamento> orçamentos = new List<Orçamento>();
                List<Pessoa> pessoas = new List<Pessoa>();

                foreach (var item in agendamentos)
                {
                    orçamentos.Add(Model1.GetOrçamento((int)item.Fk_Orçamento_Id));
                    pessoas.Add(Model1.GetPessoa(item.Fk_Pessoa_Id));
                }

                calendario.Agendamentos = agendamentos.OrderBy(e => e.Data).ToList();
                calendario.Pessoas = pessoas;
                calendario.Orçamentos = orçamentos;
            }


            return PartialView(calendario);
        }
    }
}