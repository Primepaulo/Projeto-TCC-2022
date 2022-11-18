using Projeto_TCC_2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    //Incompleto
    public class ServiçosController : DefaultController
    {
        public ActionResult VisualizarServiços(int Id)
        {
            Oficina oficina = Model1.GetOficinaById(Id);
            if (oficina == null)
            {
                return HttpNotFound();

            }
            ViewBag.Lista = Model1.GetServiços(Id);
            return View();
        }

        public ActionResult AdicionarServiços()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void AdicionarServiço()
        {
            Model1.InsertServiços(UserID, Request["Nome"], Request["Descrição"], Convert.ToDecimal(Request["Preço"]));
            Response.Redirect("/Serviços/");
        }

        public ActionResult EditarServiço(int Id)
        {
            //TBA
            Serviço serviço = Model1.GetServiço(Id);
            Oficina oficina = Model1.GetOficinaById(Id);
            return View(oficina);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarServiço([Bind(Include = "Id, Nome, Descrição, Preço, Fk_Oficina_Id")] Serviço serviço)
        {
            //TBA
            if (ModelState.IsValid)
            {
                Model1.UpdateServiço(serviço);
                return RedirectToAction("VisualizarServiços/" + serviço.Id);
            }
            return View(serviço);
        }
    }
}