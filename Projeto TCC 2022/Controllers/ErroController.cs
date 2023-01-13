using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class ErroController : DataController
    {
        public ActionResult Erro(string Erro)
        {
            return View(Erro);
        }
    }
}