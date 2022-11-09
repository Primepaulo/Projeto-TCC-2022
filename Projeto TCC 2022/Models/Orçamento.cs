namespace Projeto_TCC_2022.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orçamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orçamento()
        {
            ItemOrçamento = new HashSet<ItemOrçamento>();
        }

        [Column(TypeName = "money")]
        public decimal Valor { get; set; }

        public int Status { get; set; }

        [Key]
        public int Id { get; set; }

        public int fk_Pessoa_Id { get; set; }

        public int fk_Oficina_Id { get; set; }

        [Required]
        [StringLength(7)]
        public string fk_Carro_Placa { get; set; }

        public virtual Carro Carro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemOrçamento> ItemOrçamento { get; set; }

        public virtual Oficina Oficina { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}
