using System.Collections.Generic;

namespace Projeto_TCC_2022.Models.Classes
{
    public class ListItemViewModel
    {
        public int OrçamentoId { get; set; }
        public List<ItemViewModel> Itens { get; set; }
        public string Final { get; set; }
    }
}