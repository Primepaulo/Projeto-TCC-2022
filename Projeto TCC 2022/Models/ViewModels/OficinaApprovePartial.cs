namespace Projeto_TCC_2022.Models.Classes
{
    public class OficinaApprovePartial
    {
        public int Id { get; set; }
        public bool Marcar { get; set; }

        public Orçamento Orçamento { get; set; }
    }
}