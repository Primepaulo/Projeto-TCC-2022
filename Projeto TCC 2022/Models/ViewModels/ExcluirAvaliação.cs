using System.Collections.Generic;

namespace Projeto_TCC_2022.Models.ViewModels
{
    public class ExcluirAvaliação
    {
        public Avaliação Avaliação { get; set; }

        public List<ItemAvaliação> Itens { get; set; }

        public Oficina Oficina { get; set; }

        public List<Serviço> Serviços { get; set; }
    }
}