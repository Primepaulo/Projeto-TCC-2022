namespace Projeto_TCC_2022.Models
{
    using Projeto_TCC_2022.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Serviço
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Serviço()
        {
            Avaliação = new HashSet<Avaliação>();
            ItemOrçamento = new HashSet<ItemOrçamento>();
        }

        public int Id { get; set; }

        public int Fk_Oficina_Id { get; set; }

        [Required]
        public string Descrição { get; set; }

        [Column(TypeName = "money")]
        public decimal Preço { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Avaliação> Avaliação { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ItemOrçamento> ItemOrçamento { get; set; }

        public virtual Oficina Oficina { get; set; }
    }
}
