﻿using Projeto_TCC_2022.Models;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class NumeroController : DataController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.éPessoa = Pessoa;
            ViewBag.éOficina = Oficina;
            ViewBag.éAdmin = Admin;
            ViewBag.userID = UserID;
            ViewBag.Categorias = Categorias;
            base.OnActionExecuting(filterContext);

            if (Oficina == false && Pessoa == false)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Index");
            }
        }
        public ActionResult CadastroNúmero()
        {
            ViewBag.Novo = Model1.GetCelularTelefones(UserID);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarNúmero(string CelularTelefone1)
        {
            if (CelularTelefone1.StartsWith("0"))
            {
                CelularTelefone1 = CelularTelefone1.Split('0')[1];
            }

            Model1.InsertCelular(CelularTelefone1, UserID);
            Response.Clear();

            if (Pessoa == true)
                return RedirectToAction("CadastroVeículos", "Veículos");
            else if (Oficina == true)
                return RedirectToAction("AdicionarServiços", "Serviços");
            else
                return View();
        }
    }
}