using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}