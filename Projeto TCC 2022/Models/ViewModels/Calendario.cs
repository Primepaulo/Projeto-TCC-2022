using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_TCC_2022.Models.ViewModels
{
    public class Calendario
    {
        public Oficina Oficina { get; set; }
        public List<Agendamento> Agendamentos { get; set; }
        public bool Value { get; set; }
        public int? Dia { get; set; }

        public int? Skip { get; set; }
    }
}