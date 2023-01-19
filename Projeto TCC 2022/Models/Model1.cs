using Projeto_TCC_2022.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

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
        public virtual DbSet<Agendamento> Agendamento { get; set; }
        public virtual DbSet<ItemAvaliação> ItemAvaliação { get; set; }
        public virtual DbSet<Carro> Carro { get; set; }
        public virtual DbSet<CelularTelefone> CelularTelefone { get; set; }
        public virtual DbSet<Oficina> Oficina { get; set; }
        public virtual DbSet<Orçamento> Orçamento { get; set; }
        public virtual DbSet<Peça> Peça { get; set; }
        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<Serviço> Serviços { get; set; }
        public virtual DbSet<Imagem> Imagem { get; set; }
        public virtual DbSet<ItemOrçamento> ItemOrçamento { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Notificação> Notificação { get; set; }

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
               .HasMany(e => e.Serviço)
               .WithRequired(e => e.Oficina)
               .HasForeignKey(e => e.Fk_Oficina_Id)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Oficina>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.Oficina)
                .HasForeignKey(e => e.Fk_Oficina_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Oficina>()
                .HasMany(e => e.Agendamento)
                .WithRequired(e => e.Oficina)
                .HasForeignKey(e => e.Fk_Oficina_Id)
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
                .HasMany(e => e.Agendamento)
                .WithOptional(e => e.Orçamento)
                .HasForeignKey(e => e.Fk_Orçamento_Id);
                

            modelBuilder.Entity<Orçamento>()
                .HasMany(e => e.ItemOrçamento)
                .WithRequired(e => e.Orçamento)
                .HasForeignKey(e => e.Fk_Orçamento_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orçamento>()
                .HasMany(e => e.Notificação)
                .WithRequired(e => e.Orçamento)
                .HasForeignKey(e => e.Fk_Orçamento_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orçamento>()
                .HasMany(e => e.Avaliação)
                .WithRequired(e => e.Orçamento)
                .HasForeignKey(e => e.Fk_Orçamento_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Peça>()
                .Property(e => e.PreçoMin)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Peça>()
                .Property(e => e.PreçoMax)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ItemOrçamento>()
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
                .Property(e => e.PreçoMin)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Serviço>()
                .Property(e => e.PreçoMax)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Serviço>()
                .HasMany(e => e.ItemAvaliação)
                .WithRequired(e => e.Serviço)
                .HasForeignKey(e => e.Fk_Serviço_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Avaliação>()
                .HasMany(e => e.ItemAvaliação)
                .WithRequired(e => e.Avaliação)
                .HasForeignKey(e => e.Fk_Avaliação_Id);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.Serviço)
                .WithRequired(e => e.Categoria)
                .HasForeignKey(e => e.Fk_Categoria_Id)
                .WillCascadeOnDelete(false);
        }

        //Generic Controller
        public static Administrador GetAdmin(int Id)
        {
            using (var context = new Model1())
            {
                var query = from Administrador in context.Administrador
                            where Administrador.Fk_User_Id == Id
                            select Administrador;
                var admin = query.SingleOrDefault();
                return admin;
            }
        }

        public static List<Categoria> GetCategorias()
        {
            using (var context = new Model1())
            {
                var query = from Categoria in context.Categoria
                            select Categoria;
                var categorias = query.ToList();
                return categorias;
            }
        }

        public static Categoria GetCategoriaByName(string x)
        {
            using (var context = new Model1())
            {
                var query = from Categoria in context.Categoria
                            where Categoria.Nome == x
                            select Categoria;
                var categoria = query.SingleOrDefault();
                return categoria;
            }
        }
        public static Categoria GetCategoriaById(int x)
        {
            using (var context = new Model1())
            {
                var query = from Categoria in context.Categoria
                            where Categoria.Id == x
                            select Categoria;
                var categoria = query.SingleOrDefault();
                return categoria;
            }
        }


        // ----------------------------------------------------------------------------------------------------
        //Validação Cadastro

        public static bool GetOficinaByCNPJ(string CNPJ)
        {
            using (var context = new Model1())
            {
                var query = from Oficina in context.Oficina
                            where Oficina.CNPJ == CNPJ
                            select Oficina;
                var oficina = query.SingleOrDefault();

                if (oficina != null)
                {
                    return true;
                }

                return false;
            }
        }

        public static bool GetPessoaByCPForCNPJ(string CPF, string CNPJ)
        {
            using (var context = new Model1())
            {
                var query = from Pessoa in context.Pessoa
                            where Pessoa.CPF == CPF
                            select Pessoa;
                var cpf = query.SingleOrDefault();

                var query2 = from Pessoa in context.Pessoa
                             where Pessoa.CNPJ == CNPJ
                             select Pessoa;
                var cnpj = query2.SingleOrDefault();

                if (cpf != null && CPF != "")
                {
                    return true;
                }
                else if (cnpj != null && CNPJ != "")
                {
                    return true;
                }

                return false;
            }
        }

        // ----------------------------------------------------------------------------------------------------
        //Filters

        public static List<Oficina> GetOficinaByNameOrDescImp(bool imp, string input)
        {
            using (var context = new Model1())
            {
                var query = from Oficina in context.Oficina
                            where Oficina.AceitaImportado == imp &&
                            (Oficina.Nome.Contains(input) ||
                            Oficina.Descrição.Contains(input))
                            select Oficina;
                var oficinas = query.ToList();
                return oficinas;
            }
        }

        public static List<Oficina> GetOficinaByNameOrDesc(string input)
        {
            using (var context = new Model1())
            {
                var query = from Oficina in context.Oficina
                            where Oficina.Nome.Contains(input) ||
                            Oficina.Descrição.Contains(input)
                            select Oficina;
                var oficinas = query.ToList();
                return oficinas;
            }
        }


        public static List<Serviço> GetServiçosFilterByCategoria(string categoriaNome, string input)
        {
            using (var context = new Model1())
            {
                var categoriaId = GetCategoriaByName(categoriaNome).Id;

                var query = from Serviço in context.Serviços
                            where Serviço.Fk_Categoria_Id == categoriaId &&
                            (Serviço.Nome.Contains(input) || Serviço.Descrição.Contains(input))
                            select Serviço;
                var serviços = query.ToList();
                return serviços;
            }
        }

        public static List<Serviço> GetServiçosByInput(string input)
        {
            using (var context = new Model1())
            {
                var query = from Serviço in context.Serviços
                            where Serviço.Nome.Contains(input) || Serviço.Descrição.Contains(input)
                            select Serviço;
                var serviços = query.ToList();
                return serviços;
            }
        }

        public static List<Serviço> GetServiçosByCategoria(string Categoria)
        {
            using (var context = new Model1())
            {
                var query = from Serviço in context.Serviços
                            where Serviço.Categoria.Nome == Categoria
                            select Serviço;
                var serviços = query.ToList();
                return serviços;
            }
        }

        public static List<Serviço> GetServiçosFilterByCategoriaImp(string categoriaNome, string input)
        {
            using (var context = new Model1())
            {
                List<Serviço> serviços = new List<Serviço>();
                var categoriaId = GetCategoriaByName(categoriaNome).Id;

                var query = from Serviço in context.Serviços
                            where Serviço.Fk_Categoria_Id == categoriaId &&
                            (Serviço.Nome.Contains(input) || Serviço.Descrição.Contains(input))
                            select Serviço;

                var serviçosNI = query.ToList();

                foreach (var item in serviçosNI)
                {
                    if (item.Oficina.AceitaImportado == true)
                    {
                        serviços.Add(item);
                    }
                }
                return serviços;
            }
        }

        public static List<Oficina> GetOficinaByBairro(string x)
        {
            using (var context = new Model1())
            {
                var query = from Oficina in context.Oficina
                            where Oficina.Bairro.Contains(x)
                            select Oficina;
                var oficinas = query.ToList();
                return oficinas;
            }
        }

        public static List<Oficina> GetOficinaByBairroImp(bool imp, string x)
        {
            using (var context = new Model1())
            {
                var query = from Oficina in context.Oficina
                            where Oficina.AceitaImportado == imp &&
                            Oficina.Bairro.Contains(x)
                            select Oficina;
                var oficinas = query.ToList();
                return oficinas;
            }
        }

        // ----------------------------------------------------------------------------------------------------
        // Administrador

        public static List<Oficina> GetNonApprovedOficinas()
        {
            using (var context = new Model1())
            {
                var query = from Oficina in context.Oficina
                            where Oficina.Aprovada == false && Oficina.Finalizada == false
                            select Oficina;
                var oficinas = query.ToList();
                return oficinas;
            }
        }

        public static void ApproveOficina(Oficina oficina)
        {
            using (var context = new Model1())
            {
                oficina.Aprovada = true;

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

        public static void RecusarOficina(Oficina oficina)
        {
            using (var context = new Model1())
            {
                oficina.Finalizada = true;

                context.Entry(oficina).State = EntityState.Modified;

                context.SaveChanges();
            }
        }


        public static List<Messages> GetNonReadMessages()
        {
            using (var context = new Model1())
            {
                var query = from Messages in context.Messages
                            where Messages.Lido == false
                            select Messages;
                var messages = query.ToList();
                return messages;
            }
        }

        public static List<Messages> GetReadMessages()
        {
            using (var context = new Model1())
            {
                var query = from Messages in context.Messages
                            where Messages.Lido == true
                            select Messages;
                var messages = query.ToList();
                return messages;
            }
        }

        public static Messages GetMessageById(int Id)
        {
            using (var context = new Model1())
            {
                var query = from Messages in context.Messages
                            where Messages.Id == Id
                            select Messages;
                var message = query.SingleOrDefault();
                return message;
            }
        }

        public static void MarkAsRead(int Id)
        {
            using (var context = new Model1())
            {
                var message = GetMessageById(Id);
                message.Lido = true;

                context.Entry(message).State = EntityState.Modified;

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

        public static void SendMessage(int Fk_Oficina_Id, string Texto, bool Lido)
        {
            using (var context = new Model1())
            {
                context.Messages.Add(new Messages
                {
                    Fk_Oficina_Id = Fk_Oficina_Id,
                    Texto = Texto,
                    Lido = Lido
                });

                context.SaveChanges();
            }
        }

        public static void InsertCategorias(string Nome)
        {
            using (var context = new Model1())
            {
                context.Categoria.Add(new Categoria
                {
                    Nome = Nome
                });

                context.SaveChanges();
            }
        }

        public static void UpdateCategoria(Categoria categoria)
        {
            using (var context = new Model1())
            {
                context.Entry(categoria).State = EntityState.Modified;
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        public static void DeleteCategoria(int Id)
        {
            using (var context = new Model1())
            {
                Categoria categoria = context.Categoria.Find(Id);
                context.Categoria.Remove(categoria);
                try
                {
                    context.SaveChanges();
                }

                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }


        // ----------------------------------------------------------------------------------------------------
        // Notificações
        public static void GerarNotificações(Orçamento orçamento, int userId, string Texto)
        {
            using (var context = new Model1())
            {
                context.Notificação.Add(new Notificação
                {
                    Texto = Texto,
                    Fk_Orçamento_Id = orçamento.Id,
                    Fk_User_Id = userId,
                    Lido = false
                }); ;

                context.SaveChanges();
            }
        }

        public static List<Notificação> GetNotificações(int userId)
        {
            using (var context = new Model1())
            {
                var query = from Notificação in context.Notificação
                            .Include(Notificação => Notificação.Orçamento)
                            where Notificação.Lido == false && Notificação.Fk_User_Id == userId
                            select Notificação;
                var notificações = query.ToList();
                return notificações;
            }
        }

        public static Notificação GetNotificação(int Id)
        {
            using (var context = new Model1())
            {
                var query = from Notificação in context.Notificação
                            where Notificação.Id == Id
                            select Notificação;
                var notificação = query.SingleOrDefault();
                return notificação;
            }
        }

        public static void MarcarComoLido(Notificação notificação)
        {
            using (var context = new Model1())
            {
                notificação.Lido = true;

                context.Entry(notificação).State = EntityState.Modified;

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
        // OFICINA
        public static Oficina GetOficinaById(int Id)
        {
            var context = new Model1();
            var query = from Oficina in context.Oficina
                        where Oficina.Id == Id
                        select Oficina;
            var oficina = query.SingleOrDefault();
            return oficina;

        }

        public static void InsertOficina(int Id, string Email, string CNPJ, string Nome, string CEP, string Estado, string Cidade, string Bairro,
        string Rua, int Número, string Complemento, string Descrição, bool Aprovada, bool AceitaImportado, bool Finalizada, string HorarioFuncionamento, string DiasFuncionamento)
        {
            using (var context = new Model1())
            {
                context.Oficina.Add(new Oficina
                {
                    Id = Id,
                    Email = Email,
                    CNPJ = CNPJ,
                    Nome = Nome,
                    CEP = CEP,
                    Estado = Estado,
                    Cidade = Cidade,
                    Bairro = Bairro,
                    Rua = Rua,
                    Número = Número,
                    Complemento = Complemento,
                    Descrição = Descrição,
                    Aprovada = Aprovada,
                    AceitaImportado = AceitaImportado,
                    Finalizada = Finalizada,
                    HorarioFuncionamento = HorarioFuncionamento,
                    DiasFuncionamento = DiasFuncionamento
                });
                context.SaveChanges();
            }
        }

        public static void UpdateOficina(Oficina oficina)
        {
            using (var context = new Model1())
            {
                context.Entry(oficina).State = EntityState.Modified;
                context.SaveChanges();
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
        public static Serviço GetServiço(int Id)
        {
            using (var context = new Model1())
            {
                var query = from Serviço in context.Serviços
                            where Serviço.Id == Id

                            select Serviço;
                var serviços = query.Include(serviço => serviço.Categoria).SingleOrDefault();
                return serviços;
            }
        }
        public static List<Serviço> GetServiçosByOficinaFilterByCategoria(int categoriaId, int oficinaId)
        {
            using (var context = new Model1())
            {
                var query = from Serviço in context.Serviços
                            where Serviço.Fk_Categoria_Id == categoriaId &&
                            Serviço.Fk_Oficina_Id == oficinaId
                            select Serviço;
                var serviços = query.ToList();
                return serviços;
            }
        }
        public static Serviço GetServiçoByNomeId(int UserId, string Nome)
        {
            using (var context = new Model1())
            {
                var query = from Serviço in context.Serviços
                            where Serviço.Fk_Oficina_Id == UserId &&
                            Serviço.Nome == Nome
                            select Serviço;
                var serviço = query.SingleOrDefault();
                return serviço;
            }
        }
        public static void InsertServiços(int uID, string Nome, int Categoria, string Descrição, decimal? PreçoMin, decimal? PreçoMax, bool NecessitaAvaliarVeiculo)
        {
            using (var context = new Model1())
            {
                context.Serviços.Add(new Serviço
                {
                    Fk_Oficina_Id = uID,
                    Nome = Nome,
                    Fk_Categoria_Id = Categoria,
                    Descrição = Descrição,
                    PreçoMin = PreçoMin,
                    PreçoMax = PreçoMax,
                    NecessitaAvaliarVeiculo = NecessitaAvaliarVeiculo
                });

                context.SaveChanges();
            }
        }
        public static void UpdateServiço(Serviço serviço)
        {
            using (var context = new Model1())
            {
                context.Entry(serviço).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public static void DeleteServiço(int Id)
        {
            using (var context = new Model1())
            {
                Serviço serviço = context.Serviços.Find(Id);
                context.Serviços.Remove(serviço);
                context.SaveChanges();
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

        public static Peça GetPeça(int Id)
        {
            using (var context = new Model1())
            {
                var query = from Peça in context.Peça
                            where Peça.Id == Id
                            select Peça;
                var peça = query.SingleOrDefault();
                return peça;
            }
        }

        public static Peça GetPeçaByUIDNome(int UID, string Nome)
        {
            using (var context = new Model1())
            {
                var query = from Peça in context.Peça
                            where Peça.Nome == Nome
                            && Peça.Fk_Oficina_Id == UID
                            select Peça;
                var peça = query.SingleOrDefault();
                return peça;
            }
        }

        public static void InsertPeça(string Nome, int uID, string Marca, string Código, string Descrição, decimal? PreçoMin, decimal? PreçoMax)
        {
            using (var context = new Model1())
            {
                context.Peça.Add(new Peça
                {
                    Nome = Nome,
                    Fk_Oficina_Id = uID,
                    Marca = Marca,
                    Código = Código,
                    Descrição = Descrição,
                    PreçoMin = PreçoMin,
                    PreçoMax = PreçoMax
                });

                context.SaveChanges();
            }
        }

        public static void UpdatePeça(Peça peça)
        {
            using (var context = new Model1())
            {
                context.Entry(peça).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void DeletePeça(int Id)
        {
            using (var context = new Model1())
            {
                Peça peça = context.Peça.Find(Id);
                context.Peça.Remove(peça);
                context.SaveChanges();
            }
        }

        // ----------------------------------------------------------------------------------------------------
        // Orçamento

        public static List<Orçamento> GetAllOrçamentos(int uID)
        {
            using (var context = new Model1())
            {
                var query = from Orçamento in context.Orçamento
                            join Pessoa in context.Pessoa on Orçamento.fk_Pessoa_Id equals Pessoa.Id
                            join Oficina in context.Oficina on Orçamento.fk_Oficina_Id equals Oficina.Id
                            where Orçamento.fk_Pessoa_Id == uID || Orçamento.fk_Oficina_Id == uID
                            select Orçamento;
                var orçamentos = query.OrderByDescending(d => d.Id).ToList();
                return orçamentos;
            }
        }

        public static List<Orçamento> GetOrçamentos(int uID)
        {
            using (var context = new Model1())
            {
                var query = from Orçamento in context.Orçamento
                            join Pessoa in context.Pessoa on Orçamento.fk_Pessoa_Id equals Pessoa.Id
                            join Oficina in context.Oficina on Orçamento.fk_Oficina_Id equals Oficina.Id
                            where (Orçamento.fk_Pessoa_Id == uID || Orçamento.fk_Oficina_Id == uID) && Orçamento.Status != 3 && Orçamento.Status != 4
                            select Orçamento;
                var orçamentos = query.OrderByDescending(d => DbFunctions.TruncateTime(d.Data_Orçamento)).ToList();
                return orçamentos;
            }
        }

        public static Orçamento GetOrçamento(int Id)
        {
            using (var context = new Model1())
            {
                var query = from Orçamento in context.Orçamento
                            where Orçamento.Id == Id
                            select Orçamento;
                var orçamentos = query.SingleOrDefault();
                return orçamentos;
            }
        }

        public static Orçamento CreateOrçamento(int uID, string Placa, int OficinaId, DateTime DateOrçamento, int Tipo)
        {
            using (var context = new Model1())
            {
                Orçamento orçamento = new Orçamento
                {
                    fk_Pessoa_Id = uID,
                    fk_Carro_Placa = Placa,
                    fk_Oficina_Id = OficinaId,
                    Data_Orçamento = DateOrçamento,
                    Status = 10,
                    Tipo = Tipo
                };

                context.Orçamento.Add(orçamento);

                context.SaveChanges();

                return orçamento;
            }
        }

        public static void AprovarFinalizarOrçamento(int Id, int Operação, decimal? Valor)
        {
            using (var context = new Model1())
            {
                Orçamento orçamento = GetOrçamento(Id);
                orçamento.Status = Operação;
                
                if (Valor != null)
                {
                    orçamento.Valor = Valor;
                }

                context.Entry(orçamento).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void UpdateOrçamento(Orçamento orçamento)
        {
            using (var context = new Model1())
            {
                context.Entry(orçamento).State = EntityState.Modified;
                context.SaveChanges();
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


        //Pensar em fundir e passar nome no controller

        public static ItemOrçamento GetItemOrçamentoServiço(int OrçamentoId, int ServiçoId)
        {
            using (var context = new Model1())
            {

                var query1 = from Serviço in context.Serviços
                             where Serviço.Id == ServiçoId
                             select Serviço;

                var result = query1.SingleOrDefault();

                var query = from ItemOrçamento in context.ItemOrçamento
                            where ItemOrçamento.Fk_Orçamento_Id == OrçamentoId &&
                            ItemOrçamento.Nome == result.Nome
                            select ItemOrçamento;
                var item = query.SingleOrDefault();
                return item;
            }
        }

        public static ItemOrçamento GetItemOrçamentoPeça(int OrçamentoId, int PeçaId)
        {
            using (var context = new Model1())
            {
                var query1 = from Peça in context.Peça
                             where Peça.Id == PeçaId
                             select Peça;

                var result = query1.SingleOrDefault();

                var query = from ItemOrçamento in context.ItemOrçamento
                            where ItemOrçamento.Fk_Orçamento_Id == OrçamentoId &&
                            ItemOrçamento.Nome == result.Nome
                            select ItemOrçamento;
                var item = query.SingleOrDefault();
                return item;
            }
        }

        public static ItemOrçamento GetItemOrçamentoByName(int OrçamentoId, string Nome)
        {
            using (var context = new Model1())
            {
                var query = from ItemOrçamento in context.ItemOrçamento
                            where ItemOrçamento.Fk_Orçamento_Id == OrçamentoId &&
                            ItemOrçamento.Nome == Nome
                            select ItemOrçamento;
                var item = query.SingleOrDefault();
                return item;
            }
        }

        public static void AddItemOrçamento(int Orçamento_Id, string Nome, decimal? Preço, string Descrição, double Quantidade, bool? Avaliado, int Tipo)
        {
            using (var context = new Model1())
            {
                context.ItemOrçamento.Add(new ItemOrçamento
                {
                    Fk_Orçamento_Id = Orçamento_Id,
                    Nome = Nome,
                    Preço = Preço,
                    Descrição = Descrição,
                    Quantidade = Quantidade,
                    Avaliado = Avaliado,
                    Tipo = Tipo
                });

                context.SaveChanges();
            }
        }

        public static void AddQuantidade(ItemOrçamento itemOrçamento)
        {
            using (var context = new Model1())
            {

                context.Entry(itemOrçamento).State = EntityState.Modified;
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

        public static void AdicionarPreço(ItemOrçamento itemOrçamento, decimal Valor)
        {
            using (var context = new Model1())
            {
                itemOrçamento.Preço = Valor;
                context.Entry(itemOrçamento).State = EntityState.Modified;
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


        public static void Avaliado(ItemOrçamento item)
        {
            using (var context = new Model1())
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
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
                context.SaveChanges();
            }
        }
        public static void UpdateCarro(Carro carro)
        {
            using (var context = new Model1())
            {
                context.Entry(carro).State = EntityState.Modified;
                context.SaveChanges();

            }
        }
        public static void DeleteCarro(string Placa)
        {
            using (var context = new Model1())
            {
                Carro carro = context.Carro.Find(Placa);
                context.Carro.Remove(carro);
                context.SaveChanges();
            }
        }
        // ----------------------------------------------------------------------------------------------------
        // CELULAR
        public static List<CelularTelefone> GetCelularTelefones(int Id)
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

                context.SaveChanges();

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
        public static void InsertPessoa(int Id, string Nome, string Sobrenome, string CEP, string Estado, string Cidade, string Rua,
            int Número, string Complemento, string Email, string CPF, string CNPJ, int Tipo)
        {
            using (var context = new Model1())
            {
                context.Pessoa.Add(new Pessoa
                {
                    Id = Id,
                    Nome = Nome,
                    Sobrenome = Sobrenome,
                    CEP = CEP,
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

                context.SaveChanges();
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

                context.SaveChanges();
            }
        }

        public static void UpdateImagem(Imagem imagem)
        {
            using (var context = new Model1())
            {
                context.Entry(imagem).State = EntityState.Modified;
                context.SaveChanges();

            }
        }

        //Avaliação

        public static void GerarAvaliação(Avaliação avaliação, List<AvaliarItem> AvItem)
        {
            using (var context = new Model1())
            {
                context.Avaliação.Add(avaliação);

                foreach (var item in AvItem)
                {
                    context.ItemAvaliação.Add(new ItemAvaliação
                    {
                        Estrelas = item.Estrelas,
                        Fk_Avaliação_Id = avaliação.Id,
                        Fk_Serviço_Id = item.Fk_Serviço_Id
                    });
                }
                context.SaveChanges();
            }
        }

        public static List<Avaliação> GetAvaliações(int Id)
        {
            using (var context = new Model1())
            {
                var query = from Avaliação in context.Avaliação
                            where Avaliação.fk_Pessoa_Id == Id
                            select Avaliação;

                var Avaliações = query.Include(x => x.ItemAvaliação).ToList();
                return Avaliações;
            }
        }


        public static Avaliação GetAvaliação(int Id)
        {
            using (var context = new Model1())
            {
                var query = from Avaliação in context.Avaliação
                            where Avaliação.Id == Id
                            select Avaliação;

                var Avaliação1 = query.Include(x => x.ItemAvaliação).SingleOrDefault();
                return Avaliação1;
            }
        }

        public static List<Avaliação> GetAvaliaçãoByOficinaId(int Id)
        {
            using (var context = new Model1())
            {
                var query = from Avaliação in context.Avaliação
                            where Avaliação.Fk_Oficina_Id == Id
                            select Avaliação;

                var Avaliação1 = query.ToList();
                return Avaliação1;
            }
        }

        public static List<ItemAvaliação> GetItensAvaliação(int Id)
        {
            using (var context = new Model1())
            {
                var query = from ItemAvaliação in context.ItemAvaliação
                            where ItemAvaliação.Fk_Avaliação_Id == Id
                            select ItemAvaliação;

                var ItensAvaliação = query.ToList();

                return ItensAvaliação;
            }
        }

        public static ItemAvaliação GetItemAvaliação(int Id)
        {
            using (var context = new Model1())
            {
                var query = from ItemAvaliação in context.ItemAvaliação
                            where ItemAvaliação.Id == Id
                            select ItemAvaliação;

                var Item = query.SingleOrDefault();

                return Item;
            }
        }

        public static List<ItemAvaliação> GetItemAvaliaçãoByServiçoId(int Id)
        {
            using (var context = new Model1())
            {
                var query = from ItemAvaliação in context.ItemAvaliação
                            where ItemAvaliação.Fk_Serviço_Id == Id
                            select ItemAvaliação;

                var Item = query.ToList();

                return Item;
            }
        }

        public static void UpdateAvaliação(Avaliação avaliação, List<ItemAvaliação> Itens)
        {
            using (var context = new Model1())
            {
                context.Entry(avaliação).State = EntityState.Modified;

                foreach (var item in Itens)
                {

                    UpdateItemAv(item);
                }
                context.SaveChanges();
            }
        }
        public static void UpdateItemAv(ItemAvaliação item)
        {
            using (var context = new Model1())
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public static void DeleteAvaliação(Avaliação avaliação)
        {
            using (var context = new Model1())
            {
                Avaliação Av = context.Avaliação.Find(avaliação.Id);
                context.Avaliação.Remove(Av);
                List<ItemAvaliação> Itens = Model1.GetItensAvaliação(avaliação.Id);
                foreach (var item in Itens)
                {
                    Model1.DeleteItemAv(item);
                }

                context.SaveChanges();
            }
        }

        public static void DeleteItemAv(ItemAvaliação item)
        {
            using (var context = new Model1())
            {
                ItemAvaliação I = context.ItemAvaliação.Find(item.Id);
                context.ItemAvaliação.Remove(I);
                context.SaveChanges();
            }
        }

        //----------------------------------------------------------------------------------------
        public static List<Agendamento> GetAgendamentos(int OficinaId)
        {
            using (var context = new Model1())
            {
                var query = from Agendamento in context.Agendamento
                            where Agendamento.Fk_Oficina_Id == OficinaId
                            select Agendamento;
                var List = query.ToList();
                return List;
            }
        }

        public static List<Agendamento> GetAgendamentoByDate(DateTime date)
        {
            using (var context = new Model1())
            {
                var query = from Agendamento in context.Agendamento
                            where DbFunctions.TruncateTime(Agendamento.Data) == date.Date
                            select Agendamento;
                var item = query.ToList();
                return item;
            }
        }

        public static Agendamento GetAgendamentoByOrçamento(int Id)
        {
            using (var context = new Model1())
            {
                var query = from Agendamento in context.Agendamento
                            where Agendamento.Fk_Orçamento_Id == Id
                            select Agendamento;
                var item = query.SingleOrDefault();
                return item;
            }
        }


        public static Agendamento Agendar(DateTime Data, int OficinaId, int PessoaId)
        {
            using (var context = new Model1())
            {
                Agendamento agendamento = new Agendamento
                {
                    Data = Data,
                    Fk_Oficina_Id = OficinaId,
                    Fk_Pessoa_Id = PessoaId,
                    Fk_Orçamento_Id = null
                };

                context.Agendamento.Add(agendamento);
                context.SaveChanges();

                return agendamento;
            }

        }
        public static void UpdateAgendamento(Agendamento agendamento)
        {
            using (var context = new Model1())
            {
                context.Entry(agendamento).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

    }
}



