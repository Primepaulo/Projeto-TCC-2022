using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Projeto_TCC_2022.Models
{
    public class ServiçosModels
    {
        private readonly static string _conn = @"Data Source=EN2D09466CE6722\SQLEXPRESS;Initial Catalog = TdIdD(IN305); Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preço { get; set; }

        public ServiçosModels() { }

        public ServiçosModels(int id, string nome, decimal preço)
        {
            Id = id;
            Nome = nome;
            Preço = preço;

        }


    }
}