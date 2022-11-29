using Microsoft.AspNet.Identity;
using Projeto_TCC_2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class OficinaController : DefaultController
    {
        public ActionResult Page(int Id)
        {
            Oficina oficina = Model1.GetOficinaById(Id);
            if (oficina == null)
            {
             return RedirectToAction("Index", "Home");
          
            }
            ViewBag.Oficina = oficina;
            ViewBag.Imagem = Model1.GetImagem(Id);
            ViewBag.CelularTelefone = Model1.GetCelularTelefones(Id);
            ViewBag.Email = Model1.GetEmail(Id);
            ViewBag.Serviços = Model1.GetServiços(Id);
            return View();
        }

        public ActionResult EditarOficina(int Id)
        {
            if (UserID == Id)
            {
                Oficina oficina = Model1.GetOficinaById(Id);
                Imagem imagem = Model1.GetImagem(Id);
                ViewBag.Imagem = imagem;
                return View(oficina);
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarOficina([Bind(Include = "Id,Email,CNPJ,Nome,Estado,Cidade,Rua,Número,Complemento,Bairro,Descrição,Aprovada,AceitaImportado")] Oficina oficina, string inicio, string fim)
        {
            if (UserID == oficina.Id && Oficina == true)
            {
                if (ModelState.IsValid)
                {
                    string HorarioFuncionamento = inicio + "/" + fim;
                    oficina.HorarioFuncionamento = HorarioFuncionamento;
                    Model1.UpdateOficina(oficina);
                    return RedirectToAction("Page/" + oficina.Id);
                }
            }
            return View(oficina);
        }

        public ActionResult Sugerir()
        {
            if (Oficina == true && Aprovada == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessage(string Texto)
        {
            if (Oficina == true && Aprovada == true)
            {
                Model1.SendMessage(UserID, Texto, false);
                return RedirectToAction("Sugerir");
            }

            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}