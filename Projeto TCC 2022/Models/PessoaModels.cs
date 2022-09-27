using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Projeto_TCC_2022.Models
{
    public class PessoaModels
    {
        private readonly static string _conn = @"Data Source=EN2D09466CE6722\SQLEXPRESS;Initial Catalog = TdIdD(IN305); Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Rua { get; set; }
        public int Número { get; set; }
        public string Complemento { get; set; }
        public int fk_CelularTelefone_Id { get; set; }
        public string EMail { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public int Pessoa_Tipo { get; set; }



        public PessoaModels(){}

        public PessoaModels(string placa, string cor, string modelo, decimal motorização, string marca, int fk_Pessoa_Id)
        {
            Placa = placa;
            Cor = cor;
            Modelo = modelo;
            Motorização = motorização;
            Marca = marca;
            Fk_Pessoa_Id = fk_Pessoa_Id;

        }

        

        
    }
}