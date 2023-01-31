namespace Projeto_TCC_2022.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Imagem")]
    public partial class Imagem
    {
        [Required]
        [StringLength(200)]
        public string Url { get; set; }

        [Key]
        public int Fk_Oficina_Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual Oficina Oficina { get; set; }
    }
}
