using Projeto_TCC_2022.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class ImagemController : DataController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.éPessoa = Pessoa;
            ViewBag.éOficina = Oficina;
            ViewBag.éAdmin = Admin;
            ViewBag.userID = UserID;
            ViewBag.Categorias = Categorias;
            base.OnActionExecuting(filterContext);

            if (Oficina == false)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Index");
            }
        }
        public ActionResult AdicionarImagem()
        {
            return View();
        }

        public ActionResult MudarImagem(int Id)
        {
            Imagem imagem = Model1.GetImagem(Id);
            if (imagem == null)
            {
                return HttpNotFound();
            }
            return View(imagem);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MudarImagem([Bind(Include = "Id, Url, Fk_Oficina_Id")] Imagem imagem, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (img.ContentLength > 0)
                    {
                        string tempo = DateTime.Now.Ticks.ToString();
                        System.IO.File.Delete(imagem.Url);
                        string _FileName = Path.GetFileName(img.FileName);
                        string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                        img.SaveAs(_path);

                        //Deveria ser update no banco e não salvar dnv
                        string rpath = Path.Combine("../../UploadedFiles", tempo + _FileName);
                        Model1.SalvarImagem(rpath, UserID);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                return RedirectToAction("Page", "Oficina", UserID);
            }
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //https://stackoverflow.com/questions/52466770/how-to-upload-image-using-a-form-in-asp-net-mvc
        public ActionResult AdicionarImagem(HttpPostedFileBase img)
        {
            try
            {
                if (img.ContentLength > 0)
                {
                    string tempo = DateTime.Now.Ticks.ToString();
                    string _FileName = Path.GetFileName(img.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), tempo + _FileName);
                    string rPath = Path.Combine("../../UploadedFiles", tempo + _FileName);
                    img.SaveAs(_path);
                    Model1.SalvarImagem(rPath, UserID);
                    return RedirectToAction("VisualizarServiços", "Serviços");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return View();
        }
    }
}