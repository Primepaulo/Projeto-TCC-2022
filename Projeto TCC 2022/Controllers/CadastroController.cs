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
        public ActionResult CadastrarPessoa(string Nome, string Sobrenome, string Estado, string Cidade, string Rua, int Número, string Complemento, int Tipo)
        {
            Model1.InsertPessoa(UserID, Nome, Sobrenome, Estado,
            Cidade, Rua, Número, Complemento,
            Model1.GetEmail(UserID), Request["CPF"], Request["CNPJ"], Tipo);
            if (Request["CNPJ"] != null)
            {
                return RedirectToAction("AdicionarImagem", "Imagem");
            }
            if (!Response.IsRequestBeingRedirected)
            {
                return RedirectToAction("CadastroNúmero", "Cadastro");
            }
            else
                return View();
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

            if (ViewBag.éPessoa == true)
                return RedirectToAction("/Carros/CadastroCarros");
            else if (ViewBag.éOficina == true)
                return RedirectToAction("/Serviços/");
            else
                return View();
        }

        public ActionResult CadastroOficina()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarOficina(string CNPJ, string Nome, string Estado, string Cidade, string Bairro, string Rua, int Número, string Complemento, string Descrição, bool AceitaImportado)
        {
            Model1.InsertOficina(UserID, Model1.GetEmail(UserID), CNPJ, Nome,
            Estado, Bairro, Cidade, Rua, Número,
            Complemento, Descrição, false, AceitaImportado);
            return RedirectToAction("/Cadastro/CadastroNúmero");
        }
    }
}