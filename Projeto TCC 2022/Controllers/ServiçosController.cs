using Projeto_TCC_2022.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class ServiçosController : DataController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.éPessoa = Pessoa;
            ViewBag.éOficina = Oficina;
            ViewBag.éAdmin = Admin;
            ViewBag.userID = UserID;
            ViewBag.éAprovada = Aprovada;
            ViewBag.Categorias = Categorias;
            base.OnActionExecuting(filterContext);

            if (Oficina != true)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Index");
            }
        }

        public ActionResult VisualizarServiços()
        {
            Oficina oficina = Model1.GetOficinaById(UserID);
            if (oficina == null)
            {
                return HttpNotFound();

            }

            var lista = Model1.GetServiços(oficina.Id);
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
            if (Oficina == true)
            {
                return View();
            }

            return RedirectToAction("Home", "Index");
        }

        [HttpPost]
        public ActionResult AdicionarServiço(string Nome, string Descrição, string PreçoMin, string PreçoMax)
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

            string categoria = Request.Form["categoria"];
            int categoriaId = Model1.GetCategoriaByName(categoria).Id;

            if (Model1.GetServiçoByNomeId(UserID, Nome) == null)
            {
                Model1.InsertServiços(UserID, Nome, categoriaId, Descrição, PreçoMn, PreçoMx);
            }

            return RedirectToAction("VisualizarServiços");
        }

        public ActionResult EditarServiço(int Id)
        {
            Serviço serviço = Model1.GetServiço(Id);
            if (UserID == serviço.Fk_Oficina_Id)
            {
                Categoria categoria = Model1.GetCategoriaById(serviço.Fk_Categoria_Id);

                ViewBag.sCategoria = categoria;

                return View(serviço);
            }

            return RedirectToAction("Page", "Oficina", Id);

        }

        [HttpPost]
        public ActionResult EditarServiço([Bind(Include = "Id, Nome, Descrição, Fk_Oficina_Id")] Serviço serviço, string Categoria, string PreçoMin, string PreçoMax)
        {

            if (serviço.Fk_Oficina_Id == UserID)
            {
                Categoria categoria = Model1.GetCategoriaByName(Categoria);

                serviço.Fk_Categoria_Id = categoria.Id;

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

                serviço.PreçoMin = PreçoMn;
                serviço.PreçoMax = PreçoMx;

                if (ModelState.IsValid)
                {
                    Model1.UpdateServiço(serviço);
                    return RedirectToAction("VisualizarServiços/");
                }
                return View(serviço);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeletarServiço(int Id)
        {
            Serviço serviço = Model1.GetServiço(Id);

            if (serviço.Fk_Oficina_Id == UserID)
            {
                return View(serviço);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Deletar(int Id)
        {
            Serviço serviço = Model1.GetServiço(Id);
            if (serviço.Fk_Oficina_Id == UserID)
            {
                Model1.DeleteServiço(Id);
                return RedirectToAction("VisualizarServiços");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}