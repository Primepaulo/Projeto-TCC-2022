namespace Projeto_TCC_2022.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ItemOrçamento
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Fk_Orçamento_Id { get; set; }

        public int Fk_Serviço_Id { get; set; }

        public int Fk_Peça_Id { get; set; }

        public double? Quantidade { get; set; }

        public string Descrição { get; set; }

        public virtual Orçamento Orçamento { get; set; }

        public virtual Peça Peça { get; set; }

        public virtual Serviço Serviço { get; set; }
    }
}
