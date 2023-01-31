using System.Collections.Generic;

namespace Projeto_TCC_2022.Models.ViewModels
{
    public class HistoricoAvaliação
    {
        public List<Avaliação> Av { get; set; }
        public List<ItemAvaliação> Itens { get; set; }
        public List<Oficina> Oficinas { get; set; }
        public List<Imagem> Imagens { get; set; }
        public List<Orçamento> Orçamentos { get; set; }

        public List<Serviço> Serviços { get; set; }
    }
}