using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Projeto_TCC_2022.Models
{
    public class OrçamentoModels
    {
        private readonly static string _conn = @"Data Source=EN2D09466CE6722\SQLEXPRESS;Initial Catalog = TdIdD(IN305); Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public decimal Valor { get; set; }
        public int Status { get; set; }
        public int Id_Orçamento { get; set; }
        public int Fk_Pessoa_Id { get; set; }
        public string Fk_Oficina_CNPJ { get; set; }
        public string Fk_Carro_Placa { get; set; }

        public OrçamentoModels(){}

        public OrçamentoModels(decimal valor, int status, int id_orçamento, int fk_pessoa_Id, string fk_Oficina_CNPJ, string fk_Carro_Placa)
        {
            Valor = valor;
            Status = status;
            Id_Orçamento = id_orçamento;
            Fk_Pessoa_Id = fk_pessoa_Id;
            Fk_Oficina_CNPJ = fk_Oficina_CNPJ;
            Fk_Carro_Placa = fk_Carro_Placa;

        }

    }
}