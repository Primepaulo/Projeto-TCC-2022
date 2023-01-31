using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_TCC_2022.Models.ViewModels
{
    public class Cadastro
    {
        public Oficina Oficina { get; set; }
        public Pessoa Pessoa { get; set; }
        public CelularTelefone CelularTelefone { get; set; }

        public Dias Dias { get; set; }

    }
}