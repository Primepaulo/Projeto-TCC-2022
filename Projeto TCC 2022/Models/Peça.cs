namespace Projeto_TCC_2022.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Peça
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public int Fk_Oficina_Id { get; set; }

        [Column(TypeName = "money")]
        public decimal? PreçoMin { get; set; }

        [Column(TypeName = "money")]
        public decimal? PreçoMax { get; set; }

        [StringLength(50)]
        public string Marca { get; set; }

        public string Descrição { get; set; }

        public string Código { get; set; }

        public virtual Oficina Oficina { get; set; }
    }
}
