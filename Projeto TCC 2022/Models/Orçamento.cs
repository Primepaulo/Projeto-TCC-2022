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
            Serviços = new HashSet<Serviços>();
        }

        [Column(TypeName = "money")]
        public decimal Valor { get; set; }

        public int Status { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Orçamento { get; set; }

        public int fk_Pessoa_Id { get; set; }

        public int fk_Oficina_Id { get; set; }

        [Required]
        [StringLength(7)]
        public string fk_Carro_Placa { get; set; }

        public virtual Carro Carro { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Serviços> Serviços { get; set; }
    }
}
