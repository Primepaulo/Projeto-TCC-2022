using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class ExceptionController : DataController
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
        }
        public ActionResult Index(int Id)
        {
            string Erro = "";
            switch (Id)
            {
                case 1:
                    Erro = "CPF ou CNPJ já cadastrado";
                    break;
                case 2:
                    Erro = "CNPJ já cadastrado";
                    break;

            }

            ViewBag.Erro = Erro;

            return View();
        }
    }
}