﻿using Projeto_TCC_2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

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
            ViewBag.Categorias = Categorias;
            base.OnActionExecuting(filterContext);

            if (Pessoa == true || Admin == true)
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
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarServiço(string Nome, string Descrição, decimal Preço, bool NecessitaAvaliarVeiculo)
        {
            if (Oficina == true)
            {
                string categoria = Request.Form["categoria"];
                int categoriaId = Model1.GetCategoriaByName(categoria).Id;

                Model1.InsertServiços(UserID, Nome, categoriaId, Descrição, Preço);
                return RedirectToAction("VisualizarServiços/" + UserID);
            }

            return RedirectToAction("Home", "Index");
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
        [ValidateAntiForgeryToken]
        public ActionResult EditarServiço([Bind(Include = "Id, Nome, Descrição, Preço, Fk_Oficina_Id")] Serviço serviço, string Categoria)
        {

            if (serviço.Fk_Oficina_Id == UserID)
            {
                Categoria categoria = Model1.GetCategoriaByName(Categoria);
                ViewBag.sCategoria = categoria;

                serviço.Fk_Categoria_Id = categoria.Id;

                if (ModelState.IsValid)
                {
                    Model1.UpdateServiço(serviço);
                    return RedirectToAction("VisualizarServiços/" + serviço.Fk_Oficina_Id);
                }
                return View(serviço);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeletarServiço (int Id)
        {
            Serviço serviço = Model1.GetServiço(Id);

            if (serviço.Fk_Oficina_Id == UserID)
            {
                return View(serviço);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ActionName("DeletarServiço")]
        [ValidateAntiForgeryToken]
        public ActionResult Deletar(int Id)
        {
            Serviço serviço = Model1.GetServiço(Id);
            if (serviço.Fk_Oficina_Id == UserID)
            {
                Model1.DeleteServiço(Id);
                return RedirectToAction("VisualizarServiços" + serviço.Fk_Oficina_Id);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}