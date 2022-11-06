using Microsoft.AspNet.Identity;
using Projeto_TCC_2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class OficinaController : DefaultController
    {
        public ActionResult Page(int Id)
        {
            Oficina oficina = Model1.GetOficinaById(Id);
            Imagem imagem = Model1.GetImagem(Id);
            if (oficina == null)
            {
             return HttpNotFound();
          
            }
            ViewBag.Oficina = oficina;
            ViewBag.Imagem = imagem;
            ViewBag.User = UserID;
            return View();
        }

        public ActionResult EditarOficina(int Id)
        {
            Oficina oficina = Model1.GetOficinaById(Id);
            Imagem imagem = Model1.GetImagem(Id);
            ViewBag.Imagem = imagem;
            return View(oficina);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarOficina([Bind(Include = "Id,Email,CNPJ,Nome,Estado,Cidade,Rua,Número,Complemento")] Oficina oficina)
        {
            if (ModelState.IsValid)
            {
                Model1.UpdateOficina(oficina);
                return RedirectToAction("Page/" + oficina.Id);
            }
            return View(oficina);
        }
    }
}