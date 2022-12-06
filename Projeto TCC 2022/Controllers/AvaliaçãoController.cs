using Projeto_TCC_2022.Models;
using Projeto_TCC_2022.Models.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class AvaliaçãoController : DataController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.éPessoa = Pessoa;
            ViewBag.éOficina = Oficina;
            ViewBag.éAdmin = Admin;
            ViewBag.userID = UserID;
            ViewBag.Categorias = Categorias;
            base.OnActionExecuting(filterContext);

            if (Pessoa == false)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Index");
            }
        }

        public ActionResult VisualizarAvaliações()
        {
            List<Avaliação> avaliações = Model1.GetAvaliaçõesByUserId(UserID);
            List<Serviço> serviços = new List<Serviço>();
            List<Oficina> oficinas = new List<Oficina>();

            foreach (var item in avaliações)
            {
                serviços.Add(Model1.GetServiço(item.fk_Serviços_Id));
            }

            foreach (var item in serviços)
            {
                oficinas.Add(Model1.GetOficinaById(item.Fk_Oficina_Id));
            }

            ViewBag.Avaliações = avaliações;
            ViewBag.Serviços = serviços;
            ViewBag.Oficinas = oficinas;

            return View();
        }

        public ActionResult AvaliarServiço(int Id, int OrçamentoId)
        {
            Serviço serviço = Model1.GetServiço(Id);
            Orçamento orçamento = Model1.GetOrçamento(OrçamentoId);

            ServiçoAvaliaçãoOrçamento serviçoAvaliação = new ServiçoAvaliaçãoOrçamento();
            serviçoAvaliação.serviço = serviço;
            serviçoAvaliação.orçamento = orçamento;

            return View(serviçoAvaliação);
        }

        [HttpPost]
        public ActionResult AvaliarServiço(int Estrelas, string Texto, int serviçoId, int OrçamentoId)
        {
            Orçamento orçamento = Model1.GetOrçamento(OrçamentoId);

            ItemOrçamento itemOrçamento = Model1.GetItemOrçamentoServiço(OrçamentoId, serviçoId);

            if (orçamento.fk_Pessoa_Id == UserID && orçamento.Status == 3)
            {
                Model1.Avaliar(Estrelas, Texto, serviçoId, UserID, OrçamentoId);
                Model1.Avaliado(itemOrçamento);
            }
            return RedirectToAction("VisualizarAvaliações"); 
        }

        public ActionResult EditarAvaliação(int Id)
        {
            Avaliação avaliação = Model1.GetAvaliação(Id);
            if (UserID == avaliação.fk_Pessoa_Id)
            {
                return View(avaliação);
            }

            return RedirectToAction("VisualizarAvaliações", "Avaliação");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarAvaliação([Bind(Include = "Id,Estrelas,Texto,fk_Serviços_Id,, fk_Pessoa_Id, Fk_Orçamento_Id")] Avaliação avaliação)
        {
            if (avaliação.fk_Pessoa_Id == UserID)
            {
                if (ModelState.IsValid)
                {
                    Model1.UpdateAvaliação(avaliação);
                    return RedirectToAction("VisualizarAvaliações");
                }
                return View(avaliação);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeletarAvaliação(int Id)
        {
            Avaliação avaliação = Model1.GetAvaliação(Id);
            Serviço serviço = Model1.GetServiço(avaliação.fk_Serviços_Id);
            Oficina oficina = Model1.GetOficinaById(serviço.Fk_Oficina_Id);

            if (avaliação.fk_Pessoa_Id == UserID)
            {
                ViewBag.Serviço = serviço;
                ViewBag.Oficina = oficina;
                return View(avaliação);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ActionName("DeletarAvaliação")]
        [ValidateAntiForgeryToken]
        public ActionResult Deletar(int Id)
        {
            Avaliação avaliação = Model1.GetAvaliação(Id);
            if (avaliação.fk_Pessoa_Id == UserID)
            {
                Model1.DeleteAvaliação(Id);
                return RedirectToAction("VisualizarAvaliações");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}