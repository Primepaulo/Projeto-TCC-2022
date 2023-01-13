namespace Projeto_TCC_2022.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ItemAvaliação")]
    public partial class ItemAvaliação
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        [Key]
        public int Id { get; set; }

        [Required]
        public int Estrelas { get; set; }

        public int Fk_Avaliação_Id { get; set; }

        public int Fk_Serviço_Id { get; set; }

        public virtual Serviço Serviço { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual Avaliação Avaliação { get; set; }
    }
}
