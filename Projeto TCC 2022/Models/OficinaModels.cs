using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Projeto_TCC_2022.Models
{
    public class OficinaModels
    {
        private readonly static string _conn = @"Data Source=EN2D09466CE6722\SQLEXPRESS;Initial Catalog = TdIdD(IN305); Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public string Nome { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Rua { get; set; }
        public int Número { get; set; }
        public string Complemento { get; set; }
        public int Fk_TelefoneCelular_Id { get; set; }
        public string Email { get; set; }
        public string CNPJ { get; set; }



        public OficinaModels(){}

        public OficinaModels(string nome,string estado, string cidade, string rua, int número, string complemento,
             int fk_TelefoneCelular_Id, string email, string cnpj)
        {
            Nome = nome;
            Estado = estado;
            Cidade = cidade;
            Rua = rua;
            Número = número;
            Complemento = complemento;
            Fk_TelefoneCelular_Id = fk_TelefoneCelular_Id;
            Email = email;
            CNPJ = cnpj;

        }




    }
}