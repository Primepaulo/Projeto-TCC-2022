using System.Collections.Generic;

namespace Projeto_TCC_2022.Models.ViewModels
{
    public class DetalhesOrçamento
    {
        public Orçamento Orçamento { get; set; }
        public Carro Carro { get; set; }
        public List<ItemOrçamento> ItensOrçamento { get; set; }
        public List<Peça> Peças { get; set; }
        public List<Serviço> Serviços { get; set; }
        public Oficina Oficina { get; set; }
        public Imagem Imagem { get; set; }
        public Agendamento Agendamento { get; set; }
        public List<CelularTelefone> CelOficina { get; set; }
        public List<CelularTelefone> CelPessoa { get; set; }
        public Pessoa Pessoa { get; set; }
    }
}