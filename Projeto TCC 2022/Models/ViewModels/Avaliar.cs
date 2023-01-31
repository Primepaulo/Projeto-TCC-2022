using System.Collections.Generic;

namespace Projeto_TCC_2022.Models.ViewModels
{
    public class Avaliar
    {
        public int OrçamentoId { get; set; }
        public string Texto { get; set; }
        public List<AvaliarItem> AvItem { get; set; }

        public List<Serviço> Serviços { get; set; }
        public List<ItemOrçamento> Itens { get; set; }

    }
}