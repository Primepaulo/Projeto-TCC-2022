USE [master]
GO
/****** Object:  Database [TBN(IN305)]    Script Date: 11/12/2022 10:42:11 ******/
CREATE DATABASE [TBN(IN305)]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TBN(IN305)', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TBN(IN305).mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TBN(IN305)_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TBN(IN305)_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TBN(IN305)] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TBN(IN305)].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TBN(IN305)] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TBN(IN305)] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TBN(IN305)] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TBN(IN305)] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TBN(IN305)] SET ARITHABORT OFF 
GO
ALTER DATABASE [TBN(IN305)] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [TBN(IN305)] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TBN(IN305)] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TBN(IN305)] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TBN(IN305)] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TBN(IN305)] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TBN(IN305)] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TBN(IN305)] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TBN(IN305)] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TBN(IN305)] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TBN(IN305)] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TBN(IN305)] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TBN(IN305)] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TBN(IN305)] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TBN(IN305)] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TBN(IN305)] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [TBN(IN305)] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TBN(IN305)] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TBN(IN305)] SET  MULTI_USER 
GO
ALTER DATABASE [TBN(IN305)] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TBN(IN305)] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TBN(IN305)] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TBN(IN305)] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TBN(IN305)] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TBN(IN305)] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TBN(IN305)] SET QUERY_STORE = OFF
GO
USE [TBN(IN305)]
GO
/****** Object:  Table [dbo].[Administrador]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrador](
	[Id_Administrativo] [int] IDENTITY(1,1) NOT NULL,
	[Fk_User_Id] [int] NOT NULL,
	[Nome] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Administrador] PRIMARY KEY CLUSTERED 
(
	[Id_Administrativo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Avaliação]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Avaliação](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Estrelas] [int] NOT NULL,
	[Texto] [nvarchar](250) NULL,
	[fk_Serviços_Id] [int] NOT NULL,
	[fk_Pessoa_Id] [int] NOT NULL,
	[Fk_Orçamento_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Avaliação] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carro]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carro](
	[Placa] [nvarchar](7) NOT NULL,
	[Cor] [nvarchar](15) NOT NULL,
	[Modelo] [nvarchar](25) NOT NULL,
	[Motorização] [nvarchar](3) NOT NULL,
	[Marca] [nvarchar](15) NOT NULL,
	[fk_Pessoa_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Carro] PRIMARY KEY CLUSTERED 
(
	[Placa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_dbo.Categoria] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CelularTelefone]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CelularTelefone](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[CelularTelefone1] [nvarchar](11) NOT NULL,
	[Fk_User_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.CelularTelefone] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Imagem]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Imagem](
	[Fk_Oficina_Id] [int] NOT NULL,
	[Url] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_dbo.Imagem] PRIMARY KEY CLUSTERED 
(
	[Fk_Oficina_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemOrçamento]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemOrçamento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fk_Orçamento_Id] [int] NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Preço] [money] NULL,
	[Descrição] [nchar](10) NULL,
	[Quantidade] [float] NOT NULL,
	[Avaliado] [bit] NULL,
 CONSTRAINT [PK_dbo.ItemOrçamento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Texto] [nvarchar](max) NOT NULL,
	[Fk_Oficina_Id] [int] NOT NULL,
	[Lido] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notificações]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notificações](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fk_Orçamento_Id] [int] NOT NULL,
	[Fk_User_Id] [int] NOT NULL,
	[Lido] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Notificações] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Oficina]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Oficina](
	[Id] [int] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[CNPJ] [nvarchar](14) NOT NULL UNIQUE,
	[Nome] [nvarchar](50) NOT NULL,
	[CEP] [nvarchar](9) NOT NULL,
	[Estado] [nvarchar](25) NOT NULL,
	[Cidade] [nvarchar](30) NOT NULL,
	[Rua] [nvarchar](50) NOT NULL,
	[Número] [int] NOT NULL,
	[Complemento] [nvarchar](20) NULL,
	[Bairro] [nvarchar](50) NULL,
	[Descrição] [nvarchar](max) NULL,
	[Aprovada] [bit] NOT NULL,
	[AceitaImportado] [bit] NOT NULL,
	[Finalizada] [bit] NOT NULL,
	[HorarioFuncionamento] [nvarchar](11) NOT NULL,
 CONSTRAINT [PK_dbo.Oficina] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orçamento]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orçamento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Valor] [money] NULL,
	[Status] [int] NOT NULL,
	[fk_Pessoa_Id] [int] NOT NULL,
	[fk_Oficina_Id] [int] NOT NULL,
	[fk_Carro_Placa] [nvarchar](7) NOT NULL,
	[Data_Orçamento] [datetime] NOT NULL,
	[Data_Aprovação] [datetime] NULL,
	[Tipo] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Orçamento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Peça]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Peça](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](30) NOT NULL,
	[Fk_Oficina_Id] [int] NOT NULL,
	[PreçoMin] [money] NULL,
	[PreçoMax] [money] NULL,
	[Marca] [nvarchar](50) NULL,
	[Descrição] [nvarchar](max) NULL,
	[Código] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Peça] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pessoa]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pessoa](
	[Id] [int] NOT NULL,
	[Nome] [nvarchar](30) NOT NULL,
	[Sobrenome] [nvarchar](30) NOT NULL,
	[Estado] [nvarchar](25) NOT NULL,
	[Cidade] [nvarchar](30) NOT NULL,
	[Rua] [nvarchar](50) NOT NULL,
	[Número] [int] NOT NULL,
	[Complemento] [nvarchar](30) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[CPF] [nvarchar](11) NULL UNIQUE,
	[CNPJ] [nvarchar](14) NULL UNIQUE,
	[Pessoa_TIPO] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Pessoa] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Serviço]    Script Date: 11/12/2022 10:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Serviço](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fk_Oficina_Id] [int] NOT NULL,
	[Fk_Categoria_Id] [int] NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Descrição] [nvarchar](max) NULL,
	[PreçoMin] [money] NULL,
	[PreçoMax] [money] NULL,
	[NecessitaAvaliarVeiculo] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Serviço] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 11/12/2022 10:42:11 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleId]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 11/12/2022 10:42:11 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_fk_Pessoa_Id]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_fk_Pessoa_Id] ON [dbo].[Avaliação]
(
	[fk_Pessoa_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_fk_Serviços_Id]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_fk_Serviços_Id] ON [dbo].[Avaliação]
(
	[fk_Serviços_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_fk_Pessoa_Id]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_fk_Pessoa_Id] ON [dbo].[Carro]
(
	[fk_Pessoa_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fk_Oficina_Id]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_Fk_Oficina_Id] ON [dbo].[Imagem]
(
	[Fk_Oficina_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fk_Orçamento_Id]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_Fk_Orçamento_Id] ON [dbo].[ItemOrçamento]
(
	[Fk_Orçamento_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fk_Oficina_Id]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_Fk_Oficina_Id] ON [dbo].[Messages]
(
	[Fk_Oficina_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fk_Orçamento_Id]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_Fk_Orçamento_Id] ON [dbo].[Notificações]
(
	[Fk_Orçamento_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_fk_Carro_Placa]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_fk_Carro_Placa] ON [dbo].[Orçamento]
(
	[fk_Carro_Placa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_fk_Oficina_Id]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_fk_Oficina_Id] ON [dbo].[Orçamento]
(
	[fk_Oficina_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_fk_Pessoa_Id]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_fk_Pessoa_Id] ON [dbo].[Orçamento]
(
	[fk_Pessoa_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fk_Oficina_Id]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_Fk_Oficina_Id] ON [dbo].[Peça]
(
	[Fk_Oficina_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fk_Categoria_Id]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_Fk_Categoria_Id] ON [dbo].[Serviço]
(
	[Fk_Categoria_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fk_Oficina_Id]    Script Date: 11/12/2022 10:42:11 ******/
CREATE NONCLUSTERED INDEX [IX_Fk_Oficina_Id] ON [dbo].[Serviço]
(
	[Fk_Oficina_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Avaliação]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Avaliação_dbo.Pessoa_fk_Pessoa_Id] FOREIGN KEY([fk_Pessoa_Id])
REFERENCES [dbo].[Pessoa] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Avaliação] CHECK CONSTRAINT [FK_dbo.Avaliação_dbo.Pessoa_fk_Pessoa_Id]
GO
ALTER TABLE [dbo].[Avaliação]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Avaliação_dbo.Serviço_fk_Serviços_Id] FOREIGN KEY([fk_Serviços_Id])
REFERENCES [dbo].[Serviço] ([Id])
GO
ALTER TABLE [dbo].[Avaliação] CHECK CONSTRAINT [FK_dbo.Avaliação_dbo.Serviço_fk_Serviços_Id]
GO
ALTER TABLE [dbo].[Carro]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Carro_dbo.Pessoa_fk_Pessoa_Id] FOREIGN KEY([fk_Pessoa_Id])
REFERENCES [dbo].[Pessoa] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Carro] CHECK CONSTRAINT [FK_dbo.Carro_dbo.Pessoa_fk_Pessoa_Id]
GO
ALTER TABLE [dbo].[Imagem]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Imagem_dbo.Oficina_Fk_Oficina_Id] FOREIGN KEY([Fk_Oficina_Id])
REFERENCES [dbo].[Oficina] ([Id])
GO
ALTER TABLE [dbo].[Imagem] CHECK CONSTRAINT [FK_dbo.Imagem_dbo.Oficina_Fk_Oficina_Id]
GO
ALTER TABLE [dbo].[ItemOrçamento]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ItemOrçamento_dbo.Orçamento_Fk_Orçamento_Id] FOREIGN KEY([Fk_Orçamento_Id])
REFERENCES [dbo].[Orçamento] ([Id])
GO
ALTER TABLE [dbo].[ItemOrçamento] CHECK CONSTRAINT [FK_dbo.ItemOrçamento_dbo.Orçamento_Fk_Orçamento_Id]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Messages_dbo.Oficina_Fk_Oficina_Id] FOREIGN KEY([Fk_Oficina_Id])
REFERENCES [dbo].[Oficina] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_dbo.Messages_dbo.Oficina_Fk_Oficina_Id]
GO
ALTER TABLE [dbo].[Notificações]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Notificações_dbo.Orçamento_Fk_Orçamento_Id] FOREIGN KEY([Fk_Orçamento_Id])
REFERENCES [dbo].[Orçamento] ([Id])
GO
ALTER TABLE [dbo].[Notificações] CHECK CONSTRAINT [FK_dbo.Notificações_dbo.Orçamento_Fk_Orçamento_Id]
GO
ALTER TABLE [dbo].[Orçamento]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Orçamento_dbo.Carro_fk_Carro_Placa] FOREIGN KEY([fk_Carro_Placa])
REFERENCES [dbo].[Carro] ([Placa])
GO
ALTER TABLE [dbo].[Orçamento] CHECK CONSTRAINT [FK_dbo.Orçamento_dbo.Carro_fk_Carro_Placa]
GO
ALTER TABLE [dbo].[Orçamento]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Orçamento_dbo.Oficina_fk_Oficina_Id] FOREIGN KEY([fk_Oficina_Id])
REFERENCES [dbo].[Oficina] ([Id])
GO
ALTER TABLE [dbo].[Orçamento] CHECK CONSTRAINT [FK_dbo.Orçamento_dbo.Oficina_fk_Oficina_Id]
GO
ALTER TABLE [dbo].[Orçamento]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Orçamento_dbo.Pessoa_fk_Pessoa_Id] FOREIGN KEY([fk_Pessoa_Id])
REFERENCES [dbo].[Pessoa] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orçamento] CHECK CONSTRAINT [FK_dbo.Orçamento_dbo.Pessoa_fk_Pessoa_Id]
GO
ALTER TABLE [dbo].[Peça]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Peça_dbo.Oficina_Fk_Oficina_Id] FOREIGN KEY([Fk_Oficina_Id])
REFERENCES [dbo].[Oficina] ([Id])
GO
ALTER TABLE [dbo].[Peça] CHECK CONSTRAINT [FK_dbo.Peça_dbo.Oficina_Fk_Oficina_Id]
GO
ALTER TABLE [dbo].[Serviço]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Serviço_dbo.Categoria_Fk_Categoria_Id] FOREIGN KEY([Fk_Categoria_Id])
REFERENCES [dbo].[Categoria] ([Id])
GO
ALTER TABLE [dbo].[Serviço] CHECK CONSTRAINT [FK_dbo.Serviço_dbo.Categoria_Fk_Categoria_Id]
GO
ALTER TABLE [dbo].[Serviço]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Serviço_dbo.Oficina_Fk_Oficina_Id] FOREIGN KEY([Fk_Oficina_Id])
REFERENCES [dbo].[Oficina] ([Id])
GO
ALTER TABLE [dbo].[Serviço] CHECK CONSTRAINT [FK_dbo.Serviço_dbo.Oficina_Fk_Oficina_Id]
GO
USE [master]
GO
ALTER DATABASE [TBN(IN305)] SET  READ_WRITE 
GO
USE [TBN(IN305)]
GO
Insert into [dbo].[Categoria] values ('Revisão')
Insert into [dbo].[Categoria] values ('Geometria e Balanceamento')
Insert into [dbo].[Categoria] values ('Suspensão')
Insert into [dbo].[Categoria] values ('Pneus')
Insert into [dbo].[Categoria] values ('Freios')
Insert into [dbo].[Categoria] values ('Elétrica e Ignição')
Insert into [dbo].[Categoria] values ('Motor')
Insert into [dbo].[Categoria] values ('Ar Condicionado')
Insert into [dbo].[Categoria] values ('Transmissão')
Insert into [dbo].[Categoria] values ('Funilaria')
Insert into [dbo].[Categoria] values ('Escapamento')
Insert into [dbo].[Categoria] values ('Envelopamento')
Insert into [dbo].[Categoria] values ('Blindagem')
Insert into [dbo].[Categoria] values ('Instalação GNV')
Insert into [dbo].[Categoria] values ('Acessórios')
GO
insert into [dbo].[AspNetUsers] values ('admin@gmail.com', 0, 'AC/LqVf2SzzkuzljzCwKe8hxpaqCWpV6cBPhfS64h6Zzq6cWTbwA9Oir75SUSBoklQ==',
'0e0327dd-24b6-4986-9e47-450ecafb0130', NULL, 0, 0, NULL, 1, 0, 'admin@gmail.com')
GO
insert into [dbo].[Administrador] values (1, 'Admin')
GO