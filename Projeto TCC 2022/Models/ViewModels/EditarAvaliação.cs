using System.Collections.Generic;

namespace Projeto_TCC_2022.Models.ViewModels
{
    public class EditarAvaliação
    {
        public Avaliação Avaliação { get; set; }
        public List<Serviço> Serviços { get; set; }
        public List<ItemOrçamento> Itens { get; set; }
        public int Id { get; set; }
        public string Texto { get; set; }

        public List<int> Estrelas { get; set; }
        public List<ItemAvaliação> AvItem { get; set; }
    }
}