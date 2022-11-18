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
using Projeto_TCC_2022.Models;
using System.Runtime.Remoting.Contexts;
using System.Security.Permissions;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Projeto_TCC_2022.Models
{
    
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=DefaultConnection" /*is on Web.config file at line 12 in connectionString*/)
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
        public virtual DbSet<Serviço> Serviços { get; set; }
        public virtual DbSet<Imagem> Imagem { get; set; }
        public virtual DbSet<ItemOrçamento> ItemOrçamento { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carro>()
                .HasMany(e => e.Orçamento)
                .WithRequired(e => e.Carro)
                .HasForeignKey(e => e.fk_Carro_Placa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Oficina>()
                .HasOptional(e => e.Imagem)
                .WithRequired(e => e.Oficina);

            modelBuilder.Entity<Oficina>()
                .HasMany(e => e.Orçamento)
                .WithRequired(e => e.Oficina)
                .HasForeignKey(e => e.fk_Oficina_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Oficina>()
                .HasMany(e => e.Peça)
                .WithRequired(e => e.Oficina)
                .HasForeignKey(e => e.Fk_Oficina_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orçamento>()
                .Property(e => e.Valor)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Orçamento>()
                .HasMany(e => e.ItemOrçamento)
                .WithRequired(e => e.Orçamento)
                .HasForeignKey(e => e.Fk_Orçamento_Id)
                .WillCascadeOnDelete(false);

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
                .HasMany(e => e.Orçamento)
                .WithRequired(e => e.Pessoa)
                .HasForeignKey(e => e.fk_Pessoa_Id);

            modelBuilder.Entity<Serviço>()
                .Property(e => e.Preço)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Serviço>()
                .HasMany(e => e.Avaliação)
                .WithRequired(e => e.Serviços)
                .HasForeignKey(e => e.fk_Serviços_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Serviço>()
                .HasMany(e => e.ItemOrçamento)
                .WithRequired(e => e.Serviço)
                .HasForeignKey(e => e.Fk_Serviço_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Peça>()
                .HasMany(e => e.ItemOrçamento)
                .WithRequired(e => e.Peça)
                .HasForeignKey(e => e.Fk_Peça_Id)
                .WillCascadeOnDelete(false);

        }

        // ----------------------------------------------------------------------------------------------------
        // OFICINA

        public static List<Oficina> GetOficina(string x)
        {
            using (var context = new Model1())
            {
                //Select Oficina WHERE Nome LIKE %x% e Eager Load de Serviços e Peça.
                var query = from Oficina in context.Oficina
                            where Oficina.Nome.Contains(x)
                            select Oficina;
                var oficinas = query.ToList();
                return oficinas;
            }
        }
        public static Oficina GetOficinaById(int Id)
        {
            using (var context = new Model1())
            {
                var query = from Oficina in context.Oficina
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

        // ----------------------------------------------------------------------------------------------------
        //Serviços

        public static List<Serviço> GetServiços(int Id)
        {
            using (var context = new Model1())
            {
                var query = from Serviço in context.Serviços
                            where Serviço.Fk_Oficina_Id == Id
                            select Serviço;
                var serviços = query.ToList();
                return serviços;
            }
        }

        public static void InsertServiços(int uID, string Descrição, decimal Preço)
        {
            using (var context = new Model1())
            {
                context.Serviços.Add(new Serviço
                {
                    Fk_Oficina_Id = uID,
                    Descrição = Descrição,
                    Preço = Preço
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

        public static void UpdateServiço(Serviço serviço)
        {
            using (var context = new Model1())
            {
                context.Entry(serviço).State = EntityState.Modified;
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

        public static void DeleteServiço(int Id)
        {
            using (var context = new Model1())
            {
                Serviço serviço = context.Serviços.Find(Id);
                context.Serviços.Remove(serviço);
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
        //Peça

        public static List<Peça> GetPeças(int Id)
        {
            using (var context = new Model1())
            {
                var query = from Peça in context.Peça
                            where Peça.Fk_Oficina_Id == Id
                            select Peça;
                var peças = query.ToList();
                return peças;
            }
        }

        public static void InsertPeças(int uID, string Marca, string Código, string Descrição, decimal Preço)
        {
            using (var context = new Model1())
            {
                context.Peça.Add(new Peça
                {
                    Fk_Oficina_Id = uID,
                    Marca = Marca,
                    Código = Código,
                    Descrição = Descrição,
                    Preço = Preço
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

        public static void UpdatePeça(Peça peça)
        {
            using (var context = new Model1())
            {
                context.Entry(peça).State = EntityState.Modified;
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

        public static void DeletePeça(int Id)
        {
            using (var context = new Model1())
            {
                Peça peça = context.Peça.Find(Id);
                context.Peça.Remove(peça);
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
        // Orçamento

        public static List<Orçamento> GetOrçamentos(int uID)
        {
            using (var context = new Model1())
            {
                var query = from Orçamento in context.Orçamento
                            join Pessoa in context.Pessoa on Orçamento.fk_Pessoa_Id equals Pessoa.Id
                            join Oficina in context.Oficina on Orçamento.fk_Oficina_Id equals Oficina.Id
                            where Orçamento.fk_Pessoa_Id == uID || Orçamento.fk_Oficina_Id == uID
                            select Orçamento;
                var orçamento = query.ToList();
                return orçamento;
            }
        }

        public static void CreateOrçamento(int uID, string Placa, int OficinaId, DateTime DateOrçamento, decimal Valor)
        {
            using (var context = new Model1())
            {
                context.Orçamento.Add(new Orçamento
                {
                    fk_Pessoa_Id = uID,
                    fk_Carro_Placa = Placa,
                    fk_Oficina_Id = OficinaId,
                    Data_Orçamento = DateOrçamento,
                    Data_Aprovação = null,
                    Status = 0,
                    Valor = Valor

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

        public static void AprovarOrçamento(Orçamento orçamento)
        {
            using (var context = new Model1())
            {
                context.Entry(orçamento).State = EntityState.Modified;
                try
                {
                    //Deve se mudar o Status para o certo (aka 1 para aprovado e 2 para finalizado)
                    //Deve se mudar a data de aprovação para a atual.
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
        // ItemOrçamento

        public static List<ItemOrçamento> GetItems(int Orçamento_Id)
        {
            using (var context = new Model1())
            {
                var query = from ItemOrçamento in context.ItemOrçamento
                            where ItemOrçamento.Fk_Orçamento_Id == Orçamento_Id
                            select ItemOrçamento;
                var item = query.ToList();
                return item;
            }
        }

        public static void AddItem(int Orçamento_Id, int Serviço_Id, int Peça_Id, double? quantidade, string Descrição)
        {
            using (var context = new Model1())
            {
                context.ItemOrçamento.Add(new ItemOrçamento
                {
                    Fk_Orçamento_Id = Orçamento_Id,
                    Fk_Serviço_Id = Serviço_Id,
                    Fk_Peça_Id = Peça_Id,
                    Quantidade = quantidade,
                    Descrição = Descrição

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
            public static List<CelularTelefone> GetCelularTelefones (int Id)
            {
                using (var context = new Model1())
                {
                    var query = from CelularTelefone in context.CelularTelefone
                                where CelularTelefone.Fk_User_Id == Id
                                select CelularTelefone;
                    var celularTelefones = query.ToList();
                    return celularTelefones;
                }
            }
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

            public static Pessoa GetPessoa(int uID)
            {
                using (var context = new Model1())
                {
                    var query = from pessoa in context.Pessoa
                                where pessoa.Id == uID
                                select pessoa;
                    var Pessoa = query.SingleOrDefault();
                    return Pessoa;
                }
            }

            public static string GetEmail(int uID)
            {
                using (var context = new ApplicationDbContext())
                {
                    var query = from AspNetUsers in context.Users
                                where AspNetUsers.Id == uID
                                select AspNetUsers.Email;
                    string Email = query.SingleOrDefault();

                    return Email;
                }
            }
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
                using (var context = new Model1())
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

