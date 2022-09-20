using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto_TCC_2022.Models
{
    public class Carro
    {
        private readonly static string _conn = @"Data Source=EN2D09466CE6722\SQLEXPRESS;Initial Catalog = TdIdD(IN305); Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public string Placa { get; set; }
        public string Cor { get; set; }
        public string Modelo { get; set; }
        public decimal Motorização { get; set; }
        public string Marca { get; set; }
        public int Fk_Pessoa_Id { get; set; }

        public Carro(){}

        public Carro(string placa, string cor, string modelo, decimal motorização, string marca, int fk_Pessoa_Id)
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