namespace Projeto_TCC_2022.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Avaliação
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int Estrelas { get; set; }

        [StringLength(250)]
        public string Texto { get; set; }

        public int? fk_Serviços_Id { get; set; }

        public int? fk_Pessoa_Id { get; set; }

        public virtual Serviços Serviços { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }
}
