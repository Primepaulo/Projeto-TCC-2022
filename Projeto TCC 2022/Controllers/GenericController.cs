using Microsoft.AspNet.Identity;
using Projeto_TCC_2022.Models;
using Projeto_TCC_2022.Models.A;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            ViewBag.userID = UserID;
            ViewBag.Categorias = Categorias;
            base.OnActionExecuting(filterContext);
        }

    }
}