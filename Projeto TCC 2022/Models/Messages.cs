namespace Projeto_TCC_2022.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Messages
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public int Fk_Oficina_Id { get; set; }
        public bool Lido { get; set; }
        public virtual Oficina Oficina { get; set; }
    }
}
