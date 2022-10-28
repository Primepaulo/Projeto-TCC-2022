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
    // Debug Writer, como visto no artigo: https://damieng.com/blog/2008/07/30/linq-to-sql-log-to-debug-window-file-memory-or-multiple-writers/

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=DefaultConnection" /*is on Web.config file at line 12 in connectionString*/)
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
                .Property(e => e.Motorização)
                .HasPrecision(2, 1);

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

        public static List<Carro> GetCarros(string x)
        {
            using (var context = new Model1())
            {
                var query = from Carro in context.Carro
                            join Pessoa in context.Pessoa
                            on Carro.fk_Pessoa_Id equals Pessoa.Id
                            select Carro;
                var oficinas = query.ToList();
                return oficinas;
            }
        }

        public static void InsertCelular(int Id, string CT)
        {
            using (var context = new Model1())
            {
                context.CelularTelefone.Add(new CelularTelefone
                {
                    id = Id,
                    CelularTelefone1 = CT
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
                //Colocar isso após um redirect que ocorre no botão register ou posteriormente em configurações do usuário
            }
        }


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

        public static List<Carro> GetAllCarros()
        {
             using (var context = new Model1())
             {
                    /* Create Carro
                    context.Carro.Add(new Carro
                    {
                        Placa = "123ABCD"
                    });
                    */
                    // Select * Carros
                    var query = from Carro in context.Carro select Carro;
                    var carros = query.ToList();
                    return carros;
             }
            
        }
    
    }
}
