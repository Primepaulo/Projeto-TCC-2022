﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_TCC_2022.Models
{
    public class SearchData
    {
        public string searchTerm { get; set; }
        public string categoria { get; set; }
        public string filtro { get; set; }
        public bool importado { get; set; }
    }
}