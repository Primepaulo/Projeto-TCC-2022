using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Projeto_TCC_2022.Controllers;
using Projeto_TCC_2022.Models;

namespace Aula3108.Controllers
{
    public abstract class DefaultController : Controller
    {
        public int UserID
        {
            get { return User.Identity.GetUserId<int>(); }
        }
    }
    public class CarrosController : DefaultController
    {
        public ActionResult VisualisarCarros()
        {
            ViewBag.Lista = Model1.GetCarros(UserID);
            return View();
        }
        public ActionResult CadastroCarros()
        {
            return View();
        }

        [HttpPost]
        public void CadastrarCarros()
        {
            Model1.InsertCarro(Request["Placa"], Request["Cor"], Request["Modelo"], Decimal.Parse(Request["Motorização"].Replace(".",",")), Request["Marca"], UserID);
            Response.Redirect("/Carros/");
        }
    }
}