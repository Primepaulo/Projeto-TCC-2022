namespace Projeto_TCC_2022.Models.Classes
{
    public class ServiçoAvaliaçãoOrçamento
    {
        public Serviço serviço { get; set; }
        public Avaliação Avaliação { get; set; }

        public Orçamento orçamento { get; set; }
    }
}