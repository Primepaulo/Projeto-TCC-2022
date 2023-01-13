using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_TCC_2022.Models
{
    [Table("Notificações")]
    public partial class Notificação
    {
        public int Id { get; set; }
        public int Fk_Orçamento_Id { get; set; }
        public int Fk_User_Id { get; set; }
        public bool Lido { get; set; }
        public virtual Orçamento Orçamento { get; set; }
    }
}