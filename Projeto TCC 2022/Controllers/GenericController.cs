using Microsoft.AspNet.Identity;
using Projeto_TCC_2022.Models;
using Projeto_TCC_2022.Models.ViewModels;
using System.Collections.Generic;
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

            if (dias.Dom == true)
            {
                DiasFuncionamento += "Dom";
            }
            if (dias.Seg == true)
            {
                DiasFuncionamento += ",Seg";
            }
            if (dias.Ter == true)
            {
                DiasFuncionamento += ",Ter";
            }
            if (dias.Qua == true)
            {
                DiasFuncionamento += ",Qua";
            }
            if (dias.Qui == true)
            {
                DiasFuncionamento += ",Qui";
            }
            if (dias.Sex == true)
            {
                DiasFuncionamento += ",Sex";
            }
            if (dias.Sab == true)
            {
                DiasFuncionamento += ",Sab";
            }

            return DiasFuncionamento;
        }
    }
}