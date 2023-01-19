using Projeto_TCC_2022.Models;
using Projeto_TCC_2022.Models.ViewModels;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public class CadastroController : CadController
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
        public ActionResult Cadastro(int? Id)
        {
            if (Id == 1)
            {
                Session["CadTipo"] = 1;
                return RedirectToAction("Register", "Account");
            }
            else if (Id == 2)
            {
                Session["CadTipo"] = 2;
                return RedirectToAction("Register", "Account");
            }

            return View();
        }
        public ActionResult CadastroPessoa()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarPessoa(Cadastro cadastro, int Tipo)
        {
            Pessoa pessoa = cadastro.Pessoa;
            CelularTelefone numero = cadastro.CelularTelefone;

            string CPF, CNPJ;

            if (pessoa.CPF != null)
            {
                CPF = pessoa.CPF.Replace(".", "").Replace("-", "");
            }
            else { CPF = null; }

            if (pessoa.CNPJ != null)
            {
                CNPJ = pessoa.CNPJ.Replace(".", "").Replace("-", "").Replace("/", "");
            }
            else { CNPJ = null; }

            if (pessoa.CPF != null && pessoa.CNPJ != null)
            {
                if (Tipo == 1)
                {
                    CNPJ = null;
                }
                else if (Tipo == 2)
                {
                    CPF = null;
                }

                else
                    RedirectToAction("Index", "Home");
            }


            if (numero.CelularTelefone1.StartsWith("0"))
            {
                numero.CelularTelefone1 = numero.CelularTelefone1.Split('0')[1];
            }

            if (Model1.GetPessoaByCPForCNPJ(CPF, CNPJ) == false)
            {
                Model1.InsertPessoa(UserID, pessoa.Nome, pessoa.Sobrenome, pessoa.CEP, pessoa.Estado,
                pessoa.Cidade, pessoa.Rua, pessoa.Número, pessoa.Complemento,
                Model1.GetEmail(UserID), CPF, CNPJ, Tipo);
                Model1.InsertCelular(numero.CelularTelefone1, UserID);
                Response.Clear();
                return RedirectToAction("VisualizarVeículos", "Veículos");
            }

            Response.Clear();
            string Erro = "Erro no Cadastro, CNPJ ou CPF já foi cadastrado.";
            return RedirectToAction("Erro", "Erro", new { Erro });
        }

        public ActionResult CadastroOficina()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarOficina(Cadastro cadastro, string inicio, string fim)
        {
            if (inicio == fim)
            {
                return RedirectToAction("CadastroOficina", "Cadastro");
            }

            string DiasFuncionamento = Add(cadastro.Dias);

            Oficina oficina = cadastro.Oficina;
            CelularTelefone numero = cadastro.CelularTelefone;

            var novoCNPJ = oficina.CNPJ.Replace(".", "").Replace("/", "").Replace("-", "");
            string HorarioFuncionamento = inicio + "/" + fim;

            if (numero.CelularTelefone1.StartsWith("0"))
            {
                numero.CelularTelefone1 = numero.CelularTelefone1.Split('0')[1];
            }

            if (Model1.GetOficinaByCNPJ(oficina.CNPJ) == false)
            {
                Model1.InsertOficina(UserID, Model1.GetEmail(UserID), novoCNPJ, oficina.Nome, oficina.CEP,
                oficina.Estado, oficina.Cidade, oficina.Bairro, oficina.Rua, oficina.Número,
                oficina.Complemento, oficina.Descrição, false, oficina.AceitaImportado, false, HorarioFuncionamento,
                DiasFuncionamento);

                Model1.InsertCelular(numero.CelularTelefone1, UserID);
                Response.Clear();
                return RedirectToAction("AdicionarImagem", "Imagem");
            }

            Response.Clear();
            return RedirectToAction("Erro", "Erro", new { Erro = "Erro no Cadastro, CNPJ já foi cadastrado." });
        }
    }
}