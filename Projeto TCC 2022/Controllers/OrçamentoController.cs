using Projeto_TCC_2022.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class OrçamentoController : DefaultController
    {
        public ActionResult EscolhaCarro()
        {
            ViewBag.Carros = Model1.GetCarros(UserID);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EscolherCarro()
        {
            string Placa = Request.Form["carro"];
            Carro carro = Model1.GetCarro(Placa);
            Session["carro"] = carro;
            return RedirectToAction("AgendarServiço");
        }

        public ActionResult AgendarServiço(int Id)
        {
            if (Pessoa == true)
            {
                Oficina oficina = Model1.GetOficinaById(Id);
                List<Serviço> serviços = Model1.GetServiços(Id);

                ViewBag.OrçamentoServiços = serviços;
                ViewBag.OrçamentoOficina = oficina;
                ViewBag.Carro = Session["carro"];
            }
            return View();
        }
    }
}
