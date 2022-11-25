using Projeto_TCC_2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

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

            var lista = Model1.GetServiços(Id);
            ViewBag.Lista = lista;

            List<Categoria> categorias = new List<Categoria>();

            if (ViewBag.Lista != null)
            {
                foreach (var item in ViewBag.Lista)
                {
                    var categoria = Model1.GetCategoriaById(item.Fk_Categoria_Id);
                    categorias.Add(categoria);
                }

                ViewBag.Categorias = categorias;
            }
            
            return View();
        }

        public ActionResult AdicionarServiços()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void AdicionarServiço(string Nome, string Descrição, decimal Preço)
        {
            string categoria = Request.Form["categoria"];
            int categoriaId = Model1.GetCategoriaByName(categoria).Id;

            Model1.InsertServiços(UserID, Nome, categoriaId, Descrição, Preço);
            RedirectToAction("VisualizarServiços/" + UserID);
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
        public ActionResult EditarServiço([Bind(Include = "Id, Nome, Descrição, Categoria, Preço, Fk_Oficina_Id")] Serviço serviço)
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