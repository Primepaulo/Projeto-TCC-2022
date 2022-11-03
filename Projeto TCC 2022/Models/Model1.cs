using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Data.SqlTypes;
using System.Data.Entity.Validation;

namespace Projeto_TCC_2022.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=LabVS2022" /*is on Web.config file at line 12 in connectionString*/)
            //Trocar também no IdentityModels.
        {
        }

        public virtual DbSet<Administrador> Administrador { get; set; }
        public virtual DbSet<Avaliação> Avaliação { get; set; }
        public virtual DbSet<Carro> Carro { get; set; }
        public virtual DbSet<CelularTelefone> CelularTelefone { get; set; }
        public virtual DbSet<Oficina> Oficina { get; set; }
        public virtual DbSet<Orçamento> Orçamento { get; set; }
        public virtual DbSet<Peça> Peça { get; set; }
        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<Serviços> Serviços { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carro>()
                .Property(e => e.Motorização);

            modelBuilder.Entity<Carro>()
                .HasMany(e => e.Orçamento)
                .WithRequired(e => e.Carro)
                .HasForeignKey(e => e.fk_Carro_Placa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Oficina>()
                .HasMany(e => e.CelularTelefone)
                .WithRequired(e => e.Oficina)
                .HasForeignKey(e => e.Fk_User_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orçamento>()
                .Property(e => e.Valor)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Peça>()
                .Property(e => e.Preço)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.Avaliação)
                .WithRequired(e => e.Pessoa)
                .HasForeignKey(e => e.fk_Pessoa_Id);

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.Carro)
                .WithRequired(e => e.Pessoa)
                .HasForeignKey(e => e.fk_Pessoa_Id);

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.CelularTelefone)
                .WithRequired(e => e.Pessoa)
                .HasForeignKey(e => e.Fk_User_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.Orçamento)
                .WithRequired(e => e.Pessoa)
                .HasForeignKey(e => e.fk_Pessoa_Id);

            modelBuilder.Entity<Serviços>()
                .Property(e => e.Preço)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Serviços>()
                .HasMany(e => e.Avaliação)
                .WithRequired(e => e.Serviços)
                .HasForeignKey(e => e.fk_Serviços_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Serviços>()
                .HasMany(e => e.Peça)
                .WithMany(e => e.Serviços)
                .Map(m => m.ToTable("Contém").MapLeftKey("fk_Serviços_Id").MapRightKey("fk_Peça_Id"));

            modelBuilder.Entity<Serviços>()
                .HasMany(e => e.Orçamento)
                .WithMany(e => e.Serviços)
                .Map(m => m.ToTable("Possui").MapLeftKey("fk_Serviços_Id").MapRightKey("fk_Orçamento_Id_Orçamento"));

            modelBuilder.Entity<Oficina>()
                .HasMany(e => e.Serviços)
                .WithMany(e => e.Oficina)
                .Map(m => m.ToTable("Oferece").MapLeftKey("fk_Oficina_Id").MapRightKey("fk_Serviços_Id"));

        }

        // ----------------------------------------------------------------------------------------------------
        // OFICINA

        public static List<Oficina> GetOficina(string x)
        {
            using (var context = new Model1())
            {
                //Select Oficina WHERE Nome LIKE %x% e Eager Load de Serviços e Peça.
                var query = from Oficina in context.Oficina.Include("Serviços.Peça")
                            where Oficina.Nome.Contains(x)
                            select Oficina;
                var oficinas = query.ToList();
                return oficinas;
            }
        }

        public static List<Carro> GetAllOficinas()
        {
            using (var context = new Model1())
            {
                var query = from Carro in context.Carro select Carro;
                var oficinas = query.ToList();
                return oficinas;
            }

        }

        public static void InsertOficina(int Id, string Email, string CNPJ, string Nome, string Estado, string Cidade, 
        string Rua, int Número, string Complemento)
        {
            using (var context = new Model1())
            {
                context.Oficina.Add(new Oficina
                {
                    Id = Id,
                    Email = Email,
                    CNPJ = CNPJ,
                    Nome = Nome,
                    Estado = Estado,
                    Cidade = Cidade,
                    Rua = Rua,
                    Número = Número,
                    Complemento = Complemento
                });
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Debug.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
            }
        }


        // ----------------------------------------------------------------------------------------------------
        // CARROS

        public static List<Carro> GetCarros(int uID)
        {
            using (var context = new Model1())
            {
                var query = from Carro in context.Carro
                            join Pessoa in context.Pessoa
                            on Carro.fk_Pessoa_Id equals Pessoa.Id
                            where Pessoa.Id == uID
                            select Carro;
                var oficinas = query.ToList();
                return oficinas;
            }
        }

        public static Carro GetCarro(string Placa)
        {
            using (var context = new Model1())
            {
                var query = from Carro in context.Carro
                            where Carro.Placa == Placa
                            select Carro;
                var carro = query.FirstOrDefault();
                return carro;
            }
        }
        public static void InsertCarro(string Placa, string Cor, string Modelo, string Motorização, string Marca, int uID)
        {
            using (var context = new Model1())
            {
                context.Carro.Add(new Carro
                {
                    Placa = Placa,
                    Cor = Cor,
                    Modelo = Modelo,
                    Motorização = Motorização,
                    Marca = Marca,
                    fk_Pessoa_Id = uID

                }); 
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Debug.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
            }
        }

        public static void UpdateCarro(Carro carro)
        {
            using (var context = new Model1())
            {
                context.Entry(carro).State = EntityState.Modified;
                try
                {
                      context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                   foreach (var entityValidationErrors in ex.EntityValidationErrors)
                   {
                       foreach (var validationError in entityValidationErrors.ValidationErrors)
                       {
                            Debug.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                       }
                   }
                }
            }
        }

        public static void DeleteCarro(string Placa)
        {
            using (var context = new Model1())
            {
                Carro carro = context.Carro.Find(Placa);
                context.Carro.Remove(carro);
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Debug.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
            }
        }

        //public static void UpdateCarro(string Placa, string Cor, string Modelo, decimal Motorização, string Marca, int uID)
        //{
        //    using (var context = new Model1())
        //    {
        //        /*var carro = context.Carro.FirstOrDefault(x => x.fk_Pessoa_Id == uID); //Válido pra coisas únicas, mas não pra carro. Se 1 pessoa
        //                                                                              tiver mais de 1 carro quebra.*/
        //        var carro = context.Carro.FirstOrDefault(x => x.fk_Pessoa_Id == uID && x.Placa == Placa);

        //        if (carro != null)
        //        {
        //            carro.Cor = Cor;
        //            carro.Modelo = Modelo;
        //            carro.Motorização = Motorização;
        //            carro.Marca = Marca;
        //            try
        //            {
        //                context.SaveChanges();
        //            }
        //            catch (DbEntityValidationException ex)
        //            {
        //                foreach (var entityValidationErrors in ex.EntityValidationErrors)
        //                {
        //                    foreach (var validationError in entityValidationErrors.ValidationErrors)
        //                    {
        //                        Debug.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        // ----------------------------------------------------------------------------------------------------
        // CELULAR

        public static void InsertCelular(string CT, int uID)
        {
            using (var context = new Model1())
            {
                context.CelularTelefone.Add(new CelularTelefone
                {
                    CelularTelefone1 = CT,
                    Fk_User_Id = uID

                }); 
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Debug.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
            }
        }

        // ----------------------------------------------------------------------------------------------------
        // PESSOA

        public static void InsertPessoa(int Id, string Nome, string Sobrenome, string Estado, string Cidade, string Rua,
            int Número, string Complemento, int CelId, string Email, string CPF, string CNPJ, int Tipo)
        {
            using (var context = new Model1())
            {
                context.Pessoa.Add(new Pessoa
                {
                    Id = Id,
                    Nome = Nome,
                    Sobrenome = Sobrenome,
                    Estado = Estado,
                    Cidade = Cidade,
                    Rua = Rua,
                    Número = Número,
                    Complemento = Complemento,
                    Email = Email,
                    CPF = CPF,
                    CNPJ = CNPJ,
                    Pessoa_TIPO = Tipo
                });
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            Debug.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
            }
        }
    }
}
