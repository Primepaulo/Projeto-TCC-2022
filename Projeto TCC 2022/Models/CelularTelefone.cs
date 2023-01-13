namespace Projeto_TCC_2022.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CelularTelefone")]
    public partial class CelularTelefone
    {
        public int id { get; set; }

        [Required]
        [StringLength(11)]
        public string CelularTelefone1 { get; set; }

        public int Fk_User_Id { get; set; }

    }
}
