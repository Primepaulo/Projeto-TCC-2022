﻿using System;
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
        public int Fk_CelularTelefone_Id { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public int Pessoa_Tipo { get; set; }



        public PessoaModels(){}

        public PessoaModels(int id, string nome, string sobrenome, string estado, string cidade, string rua, int número, string complemento,
             int fk_CelularTelefone_Id, string email, string cpf, string cnpj, int pessoa_tipo)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Estado = estado;
            Cidade = cidade;
            Rua = rua;
            Número = número;
            Complemento = complemento;
            Fk_CelularTelefone_Id = fk_CelularTelefone_Id;
            Email = email;
            CPF = cpf;
            CNPJ = cnpj;
            Pessoa_Tipo = pessoa_tipo;

        }

        

        
    }
}