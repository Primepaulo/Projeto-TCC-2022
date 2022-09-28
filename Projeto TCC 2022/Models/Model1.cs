using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Projeto_TCC_2022.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
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
                .WithOptional(e => e.Carro)
                .HasForeignKey(e => e.fk_Carro_Placa);

            modelBuilder.Entity<CelularTelefone>()
                .HasMany(e => e.Oficina)
                .WithOptional(e => e.CelularTelefone)
                .HasForeignKey(e => e.fk_TelefoneCelular_Id);

            modelBuilder.Entity<CelularTelefone>()
                .HasMany(e => e.Pessoa)
                .WithOptional(e => e.CelularTelefone)
                .HasForeignKey(e => e.fk_CelularTelefone_Id);

            modelBuilder.Entity<Oficina>()
                .HasMany(e => e.Administrador)
                .WithOptional(e => e.Oficina)
                .HasForeignKey(e => e.fk_Oficina_CNPJ);

            modelBuilder.Entity<Oficina>()
                .HasMany(e => e.Orçamento)
                .WithOptional(e => e.Oficina)
                .HasForeignKey(e => e.fk_Oficina_CNPJ)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Oficina>()
                .HasMany(e => e.Serviços)
                .WithMany(e => e.Oficina)
                .Map(m => m.ToTable("Oferece").MapLeftKey("fk_Oficina_CNPJ").MapRightKey("fk_Serviços_Id"));

            modelBuilder.Entity<Orçamento>()
                .Property(e => e.Valor)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Peça>()
                .Property(e => e.Preço)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.Avaliação)
                .WithOptional(e => e.Pessoa)
                .HasForeignKey(e => e.fk_Pessoa_Id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.Carro)
                .WithOptional(e => e.Pessoa)
                .HasForeignKey(e => e.fk_Pessoa_Id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Pessoa>()
                .HasMany(e => e.Orçamento)
                .WithOptional(e => e.Pessoa)
                .HasForeignKey(e => e.fk_Pessoa_Id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Serviços>()
                .Property(e => e.Preço)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Serviços>()
                .HasMany(e => e.Avaliação)
                .WithOptional(e => e.Serviços)
                .HasForeignKey(e => e.fk_Serviços_Id);

            modelBuilder.Entity<Serviços>()
                .HasMany(e => e.Peça)
                .WithMany(e => e.Serviços)
                .Map(m => m.ToTable("Contém").MapLeftKey("fk_Serviços_Id").MapRightKey("fk_Peça_Id"));

            modelBuilder.Entity<Serviços>()
                .HasMany(e => e.Orçamento)
                .WithMany(e => e.Serviços)
                .Map(m => m.ToTable("Possui").MapLeftKey("fk_Serviços_Id").MapRightKey("fk_Orçamento_Id_Orçamento"));
        }
    }
}
