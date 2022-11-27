using Projeto_TCC_2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            Model1.InsertCelular(CelularTelefone1, UserID);
            Response.Clear();

            if (ViewBag.éPessoa == true)
                return RedirectToAction("/Carros/CadastroCarros");
            else if (ViewBag.éOficina == true)
                return RedirectToAction("/Serviços/");
            else
                return View();
        }
    }
}