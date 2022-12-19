using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_TCC_2022.Models.Classes
{
    public class AddItemOrçamento2View
    {
        public int OrçamentoId { get; set; }
        public List<ItemOrçamento> Itens { get; set; }
        public Oficina Oficina { get; set; }
        public List<Peça> Peças { get; set; }
        public List<Categoria> Categorias { get; set; }
        public List<Serviço> Padrão { get; set; }
    }
}