using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Projeto_TCC_2022.Controllers;
using Projeto_TCC_2022.Models;

namespace Projeto_TCC_2022.Controllers
{
    public class CarrosController : DataController
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
        public ActionResult VisualizarCarros()
        {
            ViewBag.Lista = Model1.GetCarros(UserID);
            return View();
        }
        public ActionResult CadastroCarros()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarCarros(string Placa, string Cor, string Modelo, string Motorização, string Marca)
        {
            Model1.InsertCarro(Placa, Cor, Modelo, Motorização, Marca, UserID);
            return RedirectToAction("VisualizarCarros");
        }

        public ActionResult EditarCarro(string Placa)
        {
            if (Placa == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = Model1.GetCarro(Placa);
            if (carro == null)
            {
                return HttpNotFound();
            }
            return View(carro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCarro([Bind(Include = "Placa,Cor,Modelo,Motorização,Marca, fk_Pessoa_Id")] Carro carro)
        {
            if (ModelState.IsValid)
            {
                Model1.UpdateCarro(carro);
                return RedirectToAction("VisualizarCarros");
            }
            return View(carro);
        }

        public ActionResult DeletarCarro(string Placa)
        {
            if (Placa == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = Model1.GetCarro(Placa);
            if (carro == null)
            {
                return HttpNotFound();
            }
            return View(carro);
        }

        [HttpPost, ActionName("DeletarCarro")]
        [ValidateAntiForgeryToken]
        public ActionResult Deletar(string Placa)
        {
            Model1.DeleteCarro(Placa);
            return RedirectToAction("VisualizarCarros");
        }

    }
}