using Microsoft.AspNet.Identity;
using Projeto_TCC_2022.Models;
using Projeto_TCC_2022.Models.ViewModels;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Controllers
{
    public abstract class DataController : Controller
    {
        public int UserID
        {
            get { return User.Identity.GetUserId<int>(); }
        }

        public bool Oficina
        {
            get
            {
                if (Model1.GetOficinaById(UserID) != null)
                {
                    return true;
                }
                else return false;
            }
        }

        public bool Pessoa
        {
            get
            {
                if (Model1.GetPessoa(UserID) != null)
                {
                    return true;
                }
                else return false;
            }
        }
        public bool Admin
        {
            get
            {
                if (Model1.GetAdmin(UserID) != null)
                {
                    return true;
                }
                else return false;
            }
        }

        public bool? Aprovada
        {
            get
            {
                if (Oficina == true)
                {
                    if (Model1.GetOficinaById(UserID).Aprovada == true)
                    {
                        return true;
                    }

                    else return false;
                }

                else { return null; }
            }
        }

        public List<Categoria> Categorias
        {
            get
            {
                var categorias = Model1.GetCategorias();
                return categorias;
            }
        }
    }

    public abstract class DefaultController : DataController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.éPessoa = Pessoa;
            ViewBag.éOficina = Oficina;
            ViewBag.éAdmin = Admin;
            ViewBag.éAprovada = Aprovada;
            ViewBag.userID = UserID;
            ViewBag.Categorias = Categorias;
            base.OnActionExecuting(filterContext);
        }
    }

    public abstract class CadController : DataController
    {
        public static string Add(Dias dias)
        {
            string DiasFuncionamento = "";

            if (dias.Domingo == true)
            {
                DiasFuncionamento += "Domingo";
            }
            if (dias.Segunda == true)
            {
                DiasFuncionamento += ",Segunda";
            }
            if (dias.Terça == true)
            {
                DiasFuncionamento += ",Terça";
            }
            if (dias.Quarta == true)
            {
                DiasFuncionamento += ",Quarta";
            }
            if (dias.Quinta == true)
            {
                DiasFuncionamento += ",Quinta";
            }
            if (dias.Sexta == true)
            {
                DiasFuncionamento += ",Sexta";
            }
            if (dias.Sábado == true)
            {
                DiasFuncionamento += ",Sábado";
            }

            return DiasFuncionamento;
        }

        public static Dias Convert(string[] itens)
        {
            Dias dias = new Dias();

            if (itens.Contains("Domingo") == true)
            {
                dias.Domingo = true;
            }

            if (itens.Contains("Segunda") == true)
            {
                dias.Segunda = true;
            }

            if (itens.Contains("Terça") == true)
            {
                dias.Terça = true;
            }

            if (itens.Contains("Quarta") == true)
            {
                dias.Quarta = true;
            }

            if (itens.Contains("Quinta") == true)
            {
                dias.Quinta = true;
            }

            if (itens.Contains("Sexta") == true)
            {
                dias.Sexta = true;
            }

            if (itens.Contains("Sábado") == true)
            {
                dias.Sábado = true;
            }

            return dias;
        }
    }
}