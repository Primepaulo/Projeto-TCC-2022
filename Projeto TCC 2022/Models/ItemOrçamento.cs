namespace Projeto_TCC_2022.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ItemOrçamento
    {
        [Key]
        public int Id { get; set; }

        public int Fk_Orçamento_Id { get; set; }

        public string Nome { get; set; }

        [Column(TypeName = "money")]
        public decimal? Preço { get; set; }

        public string Descrição { get; set; }

        public double Quantidade { get; set; }

        public bool? Avaliado { get; set; }

        public int Tipo { get; set; }

        public virtual Orçamento Orçamento { get; set; }
    }
}
