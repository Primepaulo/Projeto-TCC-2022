using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Projeto_TCC_2022.Controllers;
using Projeto_TCC_2022.Models;

namespace Projeto_TCC_2022.Controllers
{
    public class CarrosController : DefaultController
    {
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
        public void CadastrarCarros()
        {
            Model1.InsertCarro(Request["Placa"], Request["Cor"], Request["Modelo"], Request["Motorização"], Request["Marca"], UserID);
            Response.Redirect("/Carros/");
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