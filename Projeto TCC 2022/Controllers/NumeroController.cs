using Projeto_TCC_2022.Models;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class NumeroController : DataController
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

            if (Oficina == false && Pessoa == false)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Index");
            }
        }
        public ActionResult CadastroNúmero()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CadastrarNúmero(string CelularTelefone1)
        {
            if (CelularTelefone1.StartsWith("0"))
            {
                CelularTelefone1 = CelularTelefone1.Split('0')[1];
            }

            Model1.InsertCelular(CelularTelefone1, UserID);
            Response.Clear();

            return RedirectToAction("VisualizarNúmeros");
        }

        public ActionResult VisualizarNúmeros()
        {
            var x = Model1.GetCelularTelefones(UserID);
            return View(x);
        }

        public ActionResult ExcluirNúmero(int Id)
        {
            var x = Model1.GetCelularTelefone(Id);
            return View(x);
        }

        [HttpPost]
        public ActionResult Deletar(int Id)
        {
            CelularTelefone celular = Model1.GetCelularTelefone(Id);
            if (celular.Fk_User_Id == UserID)
            {
                Model1.DeletarNúmero(Id);
            }

            return RedirectToAction("VisualizarNúmeros");
        }

        public ActionResult EditarNúmero(int Id)
        {
            CelularTelefone celular = Model1.GetCelularTelefone(Id);
            return PartialView(celular);
        }

        [HttpPost]
        public ActionResult Editar([Bind(Include = "Id, CelularTelefone1, Fk_User_Id")] CelularTelefone cel)
        {
            CelularTelefone val = Model1.GetCelularTelefone(cel.Id);

            if (val.Fk_User_Id == UserID && cel.Fk_User_Id == UserID)
            {
                Model1.UpdateCelularTelefone(cel);
            }

            return RedirectToAction("VisualizarNúmeros");
        }

    }
}