using Microsoft.AspNet.Identity;
using Projeto_TCC_2022.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public abstract class DefaultController : Controller
    {
        public int UserID
        {
            get { return User.Identity.GetUserId<int>(); }
        }
    }
    public class CadastroController : DefaultController
    {
        public ActionResult Cadastro()
        {
            return View();
        }
        public ActionResult CadastroPessoa()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void CadastrarPessoa()
        {
            Model1.InsertPessoa(UserID, Request["Nome"], Request["Sobrenome"], Request["Estado"],
            Request["Cidade"], Request["Rua"], Convert.ToInt32(Request["Número"]), Request["Complemento"],
            UserID, Request["Email"], Request["CPF"], Request["CNPJ"], Convert.ToInt32(Request["Tipo"]));
            Response.Redirect("/Cadastro/CadastroNúmero");
        }

        public ActionResult CadastroNúmero()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void CadastrarNúmero()
        {
            Model1.InsertCelular(Request["Número"], UserID);

            /*Pensar em algo tipo
            if (página anterior == Pessoa)*/
              Response.Redirect("/Carros/CadastroCarros");
            /*
            else if (página anterior == Oficina)
              Response Redirect("Algum outro treco");*/
        }

        public ActionResult CadastroOficina()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void CadastrarOficina()
        {
            Model1.InsertOficina(UserID, Request["Email"], Request["CNPJ"], Request["Nome"],
            Request["Estado"], Request["Cidade"], Request["Rua"], Convert.ToInt32(Request["Número"]),
            Request["Complemento"]);
            Response.Redirect("/Cadastro/CadastroNúmero");
        }
    }
}