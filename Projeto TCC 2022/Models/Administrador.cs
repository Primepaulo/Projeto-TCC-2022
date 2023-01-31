namespace Projeto_TCC_2022.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Administrador")]
    public partial class Administrador
    {
        [Key]
        public int Id_Administrativo { get; set; }
        public int Fk_User_Id { get; set; }
        public string Nome { get; set; }
    }
}
