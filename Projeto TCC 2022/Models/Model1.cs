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
using System.IO;
using System.Drawing;

namespace Projeto_TCC_2022.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=LAB3PAULO" /*is on Web.config file at line 12 in connectionString*/)
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
        public virtual DbSet<Imagem> Imagem { get; set; }

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

            modelBuilder.Entity<Oficina>()
                .HasOptional(e => e.Images)
                .WithRequired(e => e.Oficina)
                .WillCascadeOnDelete(false);

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
        public static Oficina GetOficinaById (int Id)
        {
            using (var context = new Model1())
            {
                var query = from Oficina in context.Oficina.Include("Serviços.Peça")
                            where Oficina.Id == Id
                            select Oficina;
                var oficina = query.SingleOrDefault();
                return oficina;
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

        public static void UpdateOficina(Oficina oficina)
        {
            using (var context = new Model1())
            {
                context.Entry(oficina).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }


        //Serviços e Peça

        //public static List<Serviços> GetServiços(int Fk_Oficina_Id?)
        //{
        //    //???
        //    using (var context = new Model1())
        //    {
        //        var query = from Serviços in context.Serviços
        //                    where Serviços.
        //                    select Oficina;
        //        var serviços = query.ToList();
        //        return serviços;
        //    }
        //}

        public static void InsertServiços(int Id, string Nome, Decimal Preço)
        {
            using (var context = new Model1())
            {
                context.Serviços.Add(new Serviços
                {
                    Id = Id,
                    Nome = Nome,
                    Preço = Preço,
                    Oficina = new List<Oficina>
                    {
                        new Oficina
                    }
                    
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
            int Número, string Complemento, string Email, string CPF, string CNPJ, int Tipo)
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
        //---------------------------------------------------------------------------------------------------
        // Imagens

        public static Imagem GetImagem(int Id)
        {
            using (var context = new Model1())
            {
                var query = from Imagem in context.Imagem
                            where Imagem.Fk_Oficina_Id == Id
                            select Imagem;

                var imagem = query.FirstOrDefault();
                return imagem;
            }
        }
        public static void SalvarImagem(string Url, int Fk_Oficina_Id)
        {
            using(var context = new Model1())
            {
                context.Imagem.Add(new Imagem
                {
                    Url = Url,
                    Fk_Oficina_Id = Fk_Oficina_Id
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

        public static void UpdateImagem(Imagem imagem)
        {
            using (var context = new Model1())
            {
                context.Entry(imagem).State = EntityState.Modified;
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
