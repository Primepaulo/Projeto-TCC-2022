namespace Projeto_TCC_2022.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Carro")]
    public partial class Carro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Carro()
        {
            Orçamento = new HashSet<Orçamento>();
        }

        [Key]
        [StringLength(7)]
        public string Placa { get; set; }

        [Required]
        [StringLength(15)]
        public string Cor { get; set; }

        [Required]
        [StringLength(25)]
        public string Modelo { get; set; }

        public decimal Motorização { get; set; }

        [Required]
        [StringLength(15)]
        public string Marca { get; set; }

        public int? fk_Pessoa_Id { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orçamento> Orçamento { get; set; }
    }
}
