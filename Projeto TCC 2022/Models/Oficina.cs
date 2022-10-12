namespace Projeto_TCC_2022.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Oficina")]
    public partial class Oficina
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Oficina()
        {
            Administrador = new HashSet<Administrador>();
            Orçamento = new HashSet<Orçamento>();
            Serviços = new HashSet<Serviços>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(14)]
        public string CNPJ { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public int? fk_TelefoneCelular_Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Estado { get; set; }

        [Required]
        [StringLength(30)]
        public string Cidade { get; set; }

        [Required]
        [StringLength(50)]
        public string Rua { get; set; }

        public int Número { get; set; }

        [StringLength(20)]
        public string Complemento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Administrador> Administrador { get; set; }

        public virtual CelularTelefone CelularTelefone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orçamento> Orçamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Serviços> Serviços { get; set; }
    }
}
