using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_TCC_2022.Models.Classes
{
    public class ListItemViewModel
    {
        public int OrçamentoId { get; set; }
        public List<ItemViewModel> Items{ get; set; }

        public decimal Total { get; set; }
    }
}