using Projeto_TCC_2022.Models;
using System.Net;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class CarrosController : DataController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.éPessoa = Pessoa;
            ViewBag.éOficina = Oficina;
            ViewBag.éAdmin = Admin;
            ViewBag.userID = UserID;
            ViewBag.Categorias = Categorias;
            base.OnActionExecuting(filterContext);

            if (Pessoa == false)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Index");
            }
        }
        public ActionResult VisualizarCarros()
        {
            Pessoa pessoa = Model1.GetPessoa(UserID);
            if (pessoa == null)
            {
                return HttpNotFound();
            }

            ViewBag.Lista = Model1.GetCarros(pessoa.Id);

            return View();
        }
        public ActionResult CadastroCarros()
        {
            if (Pessoa == true)
            {
                return View();
            }

            return RedirectToAction("Home", "Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarCarros(string Placa, string Cor, string Modelo, string Motorização, string Marca)
        {
            if (Pessoa == true)
            {
                Model1.InsertCarro(Placa, Cor, Modelo, Motorização, Marca, UserID);
                return RedirectToAction("VisualizarCarros");
            }

            return RedirectToAction("Home", "Index");
        }

        public ActionResult EditarCarro(string Placa)
        {
            if (Placa == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = Model1.GetCarro(Placa);
            if (carro == null)
            {
                return HttpNotFound();
            }

            if (carro.fk_Pessoa_Id == UserID)
            {
                return View(carro);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCarro([Bind(Include = "Placa,Cor,Modelo,Motorização,Marca, fk_Pessoa_Id")] Carro carro)
        {
            if (carro.fk_Pessoa_Id == UserID)
            {
                if (ModelState.IsValid)
                {
                    Model1.UpdateCarro(carro);
                    return RedirectToAction("VisualizarCarros");
                }
                return View(carro);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeletarCarro(string Placa)
        {
            if (Placa == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carro carro = Model1.GetCarro(Placa);
            if (carro == null)
            {
                return HttpNotFound();
            }
            if (carro.fk_Pessoa_Id == UserID)
            {
                return View(carro);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost, ActionName("DeletarCarro")]
        [ValidateAntiForgeryToken]
        public ActionResult Deletar(string Placa)
        {
            Carro carro = Model1.GetCarro(Placa);
            if (carro.fk_Pessoa_Id == UserID)
            {
                Model1.DeleteCarro(Placa);
                return RedirectToAction("VisualizarCarros");
            }

            return RedirectToAction("Index", "Home");
        }

    }
}