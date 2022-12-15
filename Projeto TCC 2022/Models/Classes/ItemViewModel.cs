using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto_TCC_2022.Models.Classes
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public decimal Preço { get; set; }
    }
}