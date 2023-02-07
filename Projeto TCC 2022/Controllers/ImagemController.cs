using Projeto_TCC_2022.Models;
using System;
using System.Diagnostics;
using System.IO;
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
            ViewBag.éAprovada = Aprovada;
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

        public ActionResult MudarImagem()
        {
            Imagem imagem = Model1.GetImagem(UserID);
            return View(imagem);
        }


        [HttpPost]
        public ActionResult MudarImagem(HttpPostedFileBase img)
        {
            if (img.ContentLength > 0)
            {
                Imagem imagem = Model1.GetImagem(UserID);
                string tempo = DateTime.Now.Ticks.ToString();
                string _FileName = Path.GetFileName(img.FileName);
                string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), tempo + _FileName);
                img.SaveAs(_path);
                
                string rpath = Path.Combine("../../UploadedFiles", tempo + _FileName);
                
                if (imagem != null)
                {
                    string[] x = imagem.Url.Split('\\');
                    string old = Path.Combine(Server.MapPath("/UploadedFiles"), x[1]);
                    System.IO.File.Delete(old);
                    imagem.Url = rpath;
                    Model1.UpdateImagem(imagem);
                }
                
                else if (imagem == null)
                {
                    Model1.SalvarImagem(rpath, UserID);
                }
                
            }
                
            return RedirectToAction("Page", "Oficina", new { Id = UserID });
        }

        [HttpPost]
        //https://stackoverflow.com/questions/52466770/how-to-upload-image-using-a-form-in-asp-net-mvc
        public ActionResult AdicionarImagem(HttpPostedFileBase img)
        {
            
                if (img.ContentLength > 0)
                {
                    string tempo = DateTime.Now.Ticks.ToString();
                    string _FileName = Path.GetFileName(img.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), tempo + _FileName);
                    string rPath = Path.Combine("../../UploadedFiles", tempo + _FileName);
                    img.SaveAs(_path);
                    Model1.SalvarImagem(rPath, UserID);
                }
            return RedirectToAction("VisualizarServiços", "Serviços");
        
        }
    }
}