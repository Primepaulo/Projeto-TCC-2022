using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_TCC_2022.Models.Classes
{
    public class ServiçoPeçaTotal
    {
        public string[] serviços { get; set; }
        public string Total { get; set; }
        public string[] peças { get; set; }
        public int Id { get; set; }
    }
}