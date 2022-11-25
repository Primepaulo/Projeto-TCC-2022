﻿using Projeto_TCC_2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class AdministradorController : DefaultController
    {
        public ActionResult Index()
        {
            if (ViewBag.éAdmin != true)
            {
                RedirectToAction("Index", "Home");
            }

            ViewBag.Admin = Model1.GetAdmin(UserID);

            return View();
        }
        public ActionResult UnreadMessages()
        {
            var messages = Model1.GetNonReadMessages();
            ViewBag.Messages = messages;

            if (messages != null)
            {
                foreach (var item in messages)
                {
                    ViewBag.Oficinas.Add(Model1.GetOficinaById(item.Fk_Oficina_Id));
                }
            }

            return View();
        }

        public ActionResult Lido(int Id)
        {
            Model1.MarkAsRead(Id);
            return RedirectToAction("UnreadMessages");
        }

        public ActionResult Messages()
        {
            var messages = Model1.GetReadMessages();
            ViewBag.Messages = messages;

            if (messages != null)
            {
                foreach (var item in messages)
                {
                    ViewBag.Oficinas.Add(Model1.GetOficinaById(item.Fk_Oficina_Id));
                }
            }

            return View();
        }

        public ActionResult ListaOficinas()
        {
            var oficinas = Model1.GetNonApprovedOficinas();
            ViewBag.Oficinas = oficinas;
            foreach (var oficina in oficinas)
            {
                ViewBag.Imagens.Add(Model1.GetImagem(oficina.Id));
            }
            return View();
        }

        public ActionResult AprovarOficina(int Id)
        {
            Oficina oficina = Model1.GetOficinaById(Id);
            ViewBag.Oficina = oficina;
            ViewBag.Imagem = Model1.GetImagem(Id);
            ViewBag.CelularTelefone = Model1.GetCelularTelefones(Id);

            return View();
        }

    }
}