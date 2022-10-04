using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto_TCC_2022.Models;

namespace Aula3108.Controllers
{
    public class CarrosController : Controller
    {
        // GET: Carros
        public ActionResult Carros()
        {
            ViewBag.Title = "Carro";
            ViewBag.Title = "Cadastro de Carros";
            return View();
        }

        //[HttpPost]

        /*public void Salvar()
        {
            var Carro = new ();
            Carro.Placa = Request["placa"];
            Carro.Modelo = Request["modelo"];
            Carro.Cor = Request["cor"];
            Carro.Motorização = Convert.ToDecimal(Request["motorização"]);
            Carro.Marca = Request["marca"];
            Carro.Fk_Pessoa_Id = Convert.ToInt32(Request["fk_Pessoa_Id"]);
            Carro.Salvar();
            Response.Redirect("/Home/About");
        }*/

    }
}