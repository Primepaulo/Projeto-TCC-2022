namespace Projeto_TCC_2022.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Oficina")]
    public partial class Oficina
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Oficina()
        {
            Messages = new HashSet<Messages>();
            Serviço = new HashSet<Serviço>();
            Orçamento = new HashSet<Orçamento>();
            Peça = new HashSet<Peça>();
            Agendamento = new HashSet<Agendamento>();
        }

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

        [Required]
        [StringLength(9)]
        public string CEP { get; set; }

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

        [StringLength(50)]
        public string Bairro { get; set; }

        public string Descrição { get; set; }

        public bool Aprovada { get; set; }

        public bool AceitaImportado { get; set; }

        public bool Finalizada { get; set; }

        [StringLength(11)]
        public string HorarioFuncionamento { get; set; }

        public string DiasFuncionamento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orçamento> Orçamento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Serviço> Serviço { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Peça> Peça { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Messages> Messages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Agendamento> Agendamento { get; set; }
        public virtual Imagem Imagem { get; set; }
    }
}
