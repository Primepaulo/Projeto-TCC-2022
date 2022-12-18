using Projeto_TCC_2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Projeto_TCC_2022.Controllers
{
    public class PeçasController : DataController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.éPessoa = Pessoa;
            ViewBag.éOficina = Oficina;
            ViewBag.éAdmin = Admin;
            ViewBag.userID = UserID;
            ViewBag.Categorias = Categorias;
            base.OnActionExecuting(filterContext);

            if (Oficina != true)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Index");
            }
        }
        public ActionResult VisualizarPeças(int Id)
        {
            Oficina oficina = Model1.GetOficinaById(Id);
            if (oficina == null)
            {
                return HttpNotFound();

            }

            if (oficina.Id == UserID)
            {
                var lista = Model1.GetPeças(Id);
                ViewBag.Lista = lista;
                return View();
            }

            return RedirectToAction("Page", "Oficina", Id);
        }

        public ActionResult AdicionarPeça()
        {
            if (Oficina == true)
            {
                return View();
            }

            return RedirectToAction("Home", "Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarPeça(string Nome, string Marca, string Código, string Descrição, string PreçoMin, string PreçoMax)
        {
            decimal? PreçoMn = null, PreçoMx = null;
            if (PreçoMax.Contains("."))
            {
                PreçoMax = PreçoMax.Replace(".", ",");
            }

            if (PreçoMin.Contains("."))
            {
                PreçoMin = PreçoMin.Replace(".", ",");
            }

            if (PreçoMin != null)
            {
                PreçoMn = Decimal.Parse(PreçoMin);
            }

            if (PreçoMax != null)
            {
                PreçoMx = Decimal.Parse(PreçoMax);
            }

            Model1.InsertPeça(Nome, UserID, Marca, Código, Descrição, PreçoMn, PreçoMx);
            return RedirectToAction("VisualizarPeças/" + UserID);
            
        }

        public ActionResult EditarPeça(int Id)
        {
            Peça peça = Model1.GetPeça(Id);
            if (UserID == peça.Fk_Oficina_Id)
            {
                return View(peça);
            }
            return RedirectToAction("Home", "Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPeça([Bind(Include = "Id, Nome, Descrição, Fk_Oficina_Id, Marca, Código")] Peça peça, string PreçoMin, string PreçoMax)
        {
            if (peça.Fk_Oficina_Id == UserID)
            {
                decimal? PreçoMn = null, PreçoMx = null;
                if (PreçoMax.Contains("."))
                {
                    PreçoMax = PreçoMax.Replace(".", ",");
                }

                if (PreçoMin.Contains("."))
                {
                    PreçoMin = PreçoMin.Replace(".", ",");
                }

                if (PreçoMin != null)
                {
                    PreçoMn = Decimal.Parse(PreçoMin);
                }

                if (PreçoMax != null)
                {
                    PreçoMx = Decimal.Parse(PreçoMax);
                }

                peça.PreçoMin = PreçoMn;
                peça.PreçoMax = PreçoMx;

                if (ModelState.IsValid)
                {
                    Model1.UpdatePeça(peça);
                    return RedirectToAction("VisualizarPeças/" + peça.Fk_Oficina_Id);
                }
                return View(peça);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeletarPeça(int Id)
        {
            Peça peça = Model1.GetPeça(Id);
            if (UserID == peça.Fk_Oficina_Id)
            {
                return View(peça);
            }
            return RedirectToAction("Home", "Index");
        }

        [HttpPost, ActionName("DeletarPeça")]
        [ValidateAntiForgeryToken]
        public ActionResult Deletar(int Id)
        {
            Peça peça = Model1.GetPeça(Id);
            if (peça.Fk_Oficina_Id == UserID)
            {
                Model1.DeletePeça(Id);
                return RedirectToAction("VisualizarPeças" + peça.Id);
            }
            return RedirectToAction("Home", "Index");
        }
    }  
}