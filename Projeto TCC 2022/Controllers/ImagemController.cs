using Projeto_TCC_2022.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class ImagemController : DefaultController
    {
        // GET: Oficina
        public ActionResult AdicionarImagem()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdicionarImagem(HttpPostedFileBase img)
        {
            
            try
            {
                if (img.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(img.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    img.SaveAs(_path);
                    Model1.SalvarImagem(_path, UserID);
                }
            }
            catch (Exception ex)
            {

            }

            return View();
        }

        public ActionResult MudarImagem()
        {
            return View();
        }
    }
}