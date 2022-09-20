using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Projeto_TCC_2022.Models
{
    public class CarrosModels
    {
        private readonly static string _conn = @"Data Source=EN2D09466CE6722\SQLEXPRESS;Initial Catalog = TdIdD(IN305); Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public string Placa { get; set; }
        public string Cor { get; set; }
        public string Modelo { get; set; }
        public decimal Motorização { get; set; }
        public string Marca { get; set; }
        public int Fk_Pessoa_Id { get; set; }

        public CarrosModels(){}

        public CarrosModels(string placa, string cor, string modelo, decimal motorização, string marca, int fk_Pessoa_Id)
        {
            Placa = placa;
            Cor = cor;
            Modelo = modelo;
            Motorização = motorização;
            Marca = marca;
            Fk_Pessoa_Id = fk_Pessoa_Id;

        }

        public static List<CarrosModels> GetCarros()
        {
            var listaCarros = new List<CarrosModels>();
            var rSQL = "SELECT * FROM Carro";
            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(rSQL, cn))
                    {
                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                                while (dr.Read())
                                {
                                    listaCarros.Add(new CarrosModels(
                                        dr["Placa"].ToString(),
                                        dr["Modelo"].ToString(),
                                        dr["Cor"].ToString(),
                                        Convert.ToInt32(dr["Motorização"]),
                                        dr["Marca"].ToString(),
                                        Convert.ToInt32(dr["Fk_Pessoa_Id"])
                                        ));
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
            }
            return listaCarros;
        }

        public void Salvar()
        {

            var iSQL = "INSERT INTO Carro (placa, modelo, cor, motorização, marca) " +
                "VALUES(@placa, @modelo, @cor, @motorização, @marca)";
            try
            {
                using (var cn = new SqlConnection(_conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(iSQL, cn))
                    {
                        cmd.Parameters.AddWithValue("@placa", Placa);
                        cmd.Parameters.AddWithValue("@modelo", Modelo);
                        cmd.Parameters.AddWithValue("@cor", Cor);
                        cmd.Parameters.AddWithValue("@motorização", Motorização);
                        cmd.Parameters.AddWithValue("@marca", Marca);
                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Falha: " + ex.Message);
            }

        }
    }
}