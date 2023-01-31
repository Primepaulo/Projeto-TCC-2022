namespace Projeto_TCC_2022.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Agendamento")]
    public partial class Agendamento
    {
        public int Id { get; set; }

        public int Fk_Oficina_Id { get; set; }

        public int Fk_Pessoa_Id { get; set; }

        public int? Fk_Orçamento_Id { get; set; }

        public bool? Finalizado { get; set; }

        public DateTime Data { get; set; }

        public virtual Oficina Oficina { get; set; }

        public virtual Orçamento Orçamento { get; set; }
    }
}
