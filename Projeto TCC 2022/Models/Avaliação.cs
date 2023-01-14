namespace Projeto_TCC_2022.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Avaliação
    {
        public Avaliação()
        {
            ItemAvaliação = new HashSet<ItemAvaliação>();
        }
        public int Id { get; set; }

        public double Estrelas { get; set; }

        [StringLength(250)]
        public string Texto { get; set; }

        public int fk_Pessoa_Id { get; set; }

        public int Fk_Orçamento_Id { get; set; }

        public int Fk_Oficina_Id { get; set; }

        public virtual ICollection<ItemAvaliação> ItemAvaliação { get; set; }

        public virtual Orçamento Orçamento { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}
