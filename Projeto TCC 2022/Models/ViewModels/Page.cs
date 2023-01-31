using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_TCC_2022.Models.ViewModels
{
    public class Page
    {
        public Oficina Oficina { get; set; }
        public Imagem Imagem { get; set; }
        public List<CelularTelefone> Cel {get;set;}
        public string Email { get; set; }
        public List<Serviço> Serviços { get; set; }
        public double Média_Geral { get; set; }
        public List<AvItemMédia> Av { get; set; }
    }
}