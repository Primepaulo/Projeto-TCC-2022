namespace Projeto_TCC_2022.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pessoa")]
    public partial class Pessoa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pessoa()
        {
            Avaliação = new HashSet<Avaliação>();
            Carro = new HashSet<Carro>();
            CelularTelefone = new HashSet<CelularTelefone>();
            Orçamento = new HashSet<Orçamento>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Nome { get; set; }

        [Required]
        [StringLength(30)]
        public string Sobrenome { get; set; }

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

        [StringLength(30)]
        public string Complemento { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(11)]
        public string CPF { get; set; }

        [StringLength(14)]
        public string CNPJ { get; set; }

        public int Pessoa_TIPO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Avaliação> Avaliação { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carro> Carro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CelularTelefone> CelularTelefone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orçamento> Orçamento { get; set; }
    }
}
