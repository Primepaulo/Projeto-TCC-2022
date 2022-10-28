namespace Projeto_TCC_2022.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CelularTelefone")]
    public partial class CelularTelefone
    {
        public int id { get; set; }

        [Required]
        [StringLength(11)]
        public string CelularTelefone1 { get; set; }

        public int Fk_User_Id { get; set; }

        public virtual Oficina Oficina { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}
