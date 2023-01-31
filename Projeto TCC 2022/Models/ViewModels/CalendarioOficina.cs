﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_TCC_2022.Models.ViewModels
{
    public class CalendarioOficina
    {
        public Oficina Oficina { get; set; }
        public List<Orçamento> Orçamentos { get; set; }
        public List<Agendamento> Agendamentos { get; set; }
        public List<Pessoa> Pessoas { get; set; }
    }
}