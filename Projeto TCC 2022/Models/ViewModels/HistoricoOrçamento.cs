using System.Collections.Generic;

namespace Projeto_TCC_2022.Models.ViewModels
{
    public class HistoricoOrçamento
    {
        public List<Oficina> Oficinas { get; set; }
        public List<Imagem> Imagens { get; set; }
        public List<Carro> Carros { get; set; }

        public List<Orçamento> Orçamentos { get; set; }
    }
}