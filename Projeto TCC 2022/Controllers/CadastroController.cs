using Projeto_TCC_2022.Models;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class CadastroController : DataController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.éPessoa = Pessoa;
            ViewBag.éOficina = Oficina;
            ViewBag.éAdmin = Admin;
            ViewBag.userID = UserID;
            ViewBag.Categorias = Categorias;
            base.OnActionExecuting(filterContext);

            if (Pessoa == true || Oficina == true || Admin == true)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Index");
            }
        }
        public ActionResult Cadastro()
        {
            return View();
        }
        public ActionResult CadastroPessoa()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarPessoa(string Nome, string Sobrenome, string Estado, string Cidade, string Rua, int Número, string Complemento, int Tipo)
        {
            string CPF, CNPJ;
            if (Request["CPF"] != null)
            {
                CPF = Request["CPF"].Replace(".", "").Replace("-", "");
            }
            else { CPF = null; }

            if (Request["CNPJ"] != null)
            {
                CNPJ = Request["CNPJ"].Replace(".", "").Replace("-", "").Replace("/", "");
            }
            else { CNPJ = null; }

            if (Model1.GetPessoaByCPForCNPJ(CPF, CNPJ) == false)
            {
                Model1.InsertPessoa(UserID, Nome, Sobrenome, Estado,
                Cidade, Rua, Número, Complemento,
                Model1.GetEmail(UserID), CPF, CNPJ, Tipo);
                Response.Clear();
                return RedirectToAction("CadastroNúmero", "Numero");
            }

            Response.Clear();
            return RedirectToAction("Erro", "Erro", new { Erro = "Erro no Cadastro, CNPJ ou CPF já foi cadastrado." });
        }

        public ActionResult CadastroOficina()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarOficina(string CNPJ, string Nome, string CEP, string Estado, string Cidade, string Bairro, string Rua, int Número, string Complemento, string Descrição, bool AceitaImportado, string inicio, string fim)
        {
            var novoCNPJ = CNPJ.Replace(".", "").Replace("/", "").Replace("-", "");
            string HorarioFuncionamento = inicio + "/" + fim;
            if (Model1.GetOficinaByCNPJ(CNPJ) == false)
            {
                Model1.InsertOficina(UserID, Model1.GetEmail(UserID), novoCNPJ, Nome, CEP,
                Estado, Cidade, Bairro, Rua, Número,
                Complemento, Descrição, false, AceitaImportado, false, HorarioFuncionamento);
                Response.Clear();
                return RedirectToAction("AdicionarImagem", "Imagem");
            }

            Response.Clear();
            return RedirectToAction("Erro", "Erro", new { Erro = "Erro no Cadastro, CNPJ já foi cadastrado." });
        }
    }
}