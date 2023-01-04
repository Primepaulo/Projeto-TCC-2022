using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Projeto_TCC_2022.Models.Classes
{
    public class ServiçoCategoria
    {
        public Serviço Serviço { get; set; }
        public Categoria Categoria { get; set; }
        public int? Quant { get; set; }
    }
}