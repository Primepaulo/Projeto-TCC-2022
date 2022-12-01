using Projeto_TCC_2022.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class AdministradorController : DataController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.éPessoa = Pessoa;
            ViewBag.éOficina = Oficina;
            ViewBag.éAdmin = Admin;
            ViewBag.userID = UserID;
            ViewBag.Categorias = Categorias;
            base.OnActionExecuting(filterContext);

            if (Admin == false)
            {

                filterContext.HttpContext.Response.Redirect("/Home/Index");
            }
        }
        public ActionResult Index()
        {
            ViewBag.Admin = Model1.GetAdmin(UserID);

            return View();
        }
        public ActionResult UnreadMessages()
        {
            var messages = Model1.GetNonReadMessages();
            ViewBag.Messages = messages;

            if (messages != null)
            {
                foreach (var item in messages)
                {
                    List<Oficina> oficinas = new List<Oficina>();
                    oficinas.Add(Model1.GetOficinaById(item.Fk_Oficina_Id));
                    ViewBag.Oficinas = oficinas;
                }
            }

            return View();
        }

        public ActionResult Lido(int Id)
        {
            Model1.MarkAsRead(Id);
            return RedirectToAction("UnreadMessages");
        }

        public ActionResult Messages()
        {
            var messages = Model1.GetReadMessages();
            ViewBag.Messages = messages;

            if (messages != null)
            {
                List<Oficina> oficinas = new List<Oficina>();

                foreach (var item in messages)
                {
                    oficinas.Add(Model1.GetOficinaById(item.Fk_Oficina_Id));
                }

                ViewBag.Oficinas = oficinas;
            }

            return View();
        }

        public ActionResult ListaOficinas()
        {
            var oficinas = Model1.GetNonApprovedOficinas();
            ViewBag.Oficinas = oficinas;

            List<Imagem> imagens = new List<Imagem>();
            foreach (var oficina in oficinas)
            {
                imagens.Add(Model1.GetImagem(oficina.Id));
            }
            ViewBag.Imagens = imagens;

            return View();
        }

        public ActionResult AprovarOficinas(int Id)
        {
            Oficina model = Model1.GetOficinaById(Id);
            ViewBag.Imagem = Model1.GetImagem(Id);
            ViewBag.CelularTelefone = Model1.GetCelularTelefones(Id);

            return View(model);
        }

        public ActionResult Aprovar(Oficina oficina)
        {
            Model1.ApproveOficina(oficina);
            return RedirectToAction("ListaOficinas");

        }

        public ActionResult Rejeitar(Oficina oficina)
        {
            Model1.RecusarOficina(oficina);
            return RedirectToAction("ListaOficinas");

        }

        public ActionResult VisualizarCategorias()
        {
            return View();
        }

        public ActionResult AdicionarCategorias()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarCategorias(string Nome)
        {
            Model1.InsertCategorias(Nome);
            return RedirectToAction("VisualizarCategorias");
        }
    }
}