namespace Projeto_TCC_2022.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Serviço
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Serviço()
        {
            ItemAvaliação = new HashSet<ItemAvaliação>();
        }

        public int Id { get; set; }

        public int Fk_Oficina_Id { get; set; }

        public int Fk_Categoria_Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public string Descrição { get; set; }

        [Column(TypeName = "money")]
        public decimal? PreçoMin { get; set; }

        [Column(TypeName = "money")]
        public decimal? PreçoMax { get; set; }

        public bool NecessitaAvaliarVeiculo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemAvaliação> ItemAvaliação { get; set; }

        public virtual Oficina Oficina { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
