namespace Projeto_TCC_2022.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Administrador")]
    public partial class Administrador
    {
        [Key]
        [Column(Order = 0)]
        public int Id_Administrativo { get; set; }
        [Column(Order = 1)]
        public int Fk_User_Id { get; set; }
    }
}
