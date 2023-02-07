USE [master]
GO
/****** Object:  Database [TBN(IN305)]    Script Date: 24/01/2023 13:16:21 ******/
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
/****** Object:  Table [dbo].[Administrador]    Script Date: 24/01/2023 13:16:21 ******/
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
/****** Object:  Table [dbo].[Agendamento]    Script Date: 24/01/2023 13:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agendamento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fk_Oficina_Id] [int] NOT NULL,
	[Fk_Pessoa_Id] [int] NOT NULL,
	[Fk_Orçamento_Id] [int] NULL,
	[Data] [datetime] NOT NULL,
	[Finalizado] [bit] NULL,
 CONSTRAINT [PK_Agendamento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 24/01/2023 13:16:21 ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 24/01/2023 13:16:21 ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 24/01/2023 13:16:21 ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 24/01/2023 13:16:21 ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 24/01/2023 13:16:21 ******/
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
/****** Object:  Table [dbo].[Avaliação]    Script Date: 24/01/2023 13:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Avaliação](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Estrelas] [float] NOT NULL,
	[Texto] [nvarchar](250) NULL,
	[fk_Pessoa_Id] [int] NOT NULL,
	[Fk_Orçamento_Id] [int] NOT NULL,
	[Fk_Oficina_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Avaliação] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carro]    Script Date: 24/01/2023 13:16:21 ******/
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
/****** Object:  Table [dbo].[Categoria]    Script Date: 24/01/2023 13:16:21 ******/
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
/****** Object:  Table [dbo].[CelularTelefone]    Script Date: 24/01/2023 13:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CelularTelefone](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CelularTelefone1] [nvarchar](11) NOT NULL,
	[Fk_User_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.CelularTelefone] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Imagem]    Script Date: 24/01/2023 13:16:21 ******/
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
/****** Object:  Table [dbo].[ItemAvaliação]    Script Date: 24/01/2023 13:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemAvaliação](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Estrelas] [int] NOT NULL,
	[Fk_Avaliação_Id] [int] NOT NULL,
	[Fk_Serviço_Id] [int] NOT NULL,
 CONSTRAINT [PK_ItemAvaliação] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemOrçamento]    Script Date: 24/01/2023 13:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemOrçamento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fk_Orçamento_Id] [int] NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Preço] [money] NULL,
	[Descrição] [nvarchar](max) NULL,
	[Quantidade] [float] NOT NULL,
	[Avaliado] [bit] NULL,
	[Tipo] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ItemOrçamento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 24/01/2023 13:16:21 ******/
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
/****** Object:  Table [dbo].[Notificações]    Script Date: 24/01/2023 13:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notificações](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Texto] [nvarchar](max) NOT NULL,
	[Fk_Orçamento_Id] [int] NOT NULL,
	[Fk_User_Id] [int] NOT NULL,
	[Lido] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Notificações] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Oficina]    Script Date: 24/01/2023 13:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Oficina](
	[Id] [int] NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[CNPJ] [nvarchar](14) NOT NULL,
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
	[DiasFuncionamento] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Oficina] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Oficina__AA57D6B4C8E90177] UNIQUE NONCLUSTERED 
(
	[CNPJ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orçamento]    Script Date: 24/01/2023 13:16:21 ******/
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
	[Tipo] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Orçamento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Peça]    Script Date: 24/01/2023 13:16:21 ******/
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
/****** Object:  Table [dbo].[Pessoa]    Script Date: 24/01/2023 13:16:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pessoa](
	[Id] [int] NOT NULL,
	[Nome] [nvarchar](30) NOT NULL,
	[Sobrenome] [nvarchar](30) NOT NULL,
	[CEP] [nvarchar](9) NOT NULL,
	[Estado] [nvarchar](25) NOT NULL,
	[Cidade] [nvarchar](30) NOT NULL,
	[Rua] [nvarchar](50) NOT NULL,
	[Número] [int] NOT NULL,
	[Complemento] [nvarchar](30) NULL,
	[Email] [nvarchar](256) NOT NULL,
	[CPF] [nvarchar](11) NULL,
	[CNPJ] [nvarchar](14) NULL,
	[Pessoa_TIPO] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Pessoa] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Pessoa__AA57D6B4C3E8DA97] UNIQUE NONCLUSTERED 
(
	[CNPJ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Pessoa__C1F89731F418BE51] UNIQUE NONCLUSTERED 
(
	[CPF] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Serviço]    Script Date: 24/01/2023 13:16:21 ******/
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
 CONSTRAINT [PK_dbo.Serviço] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 24/01/2023 13:16:21 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 24/01/2023 13:16:21 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 24/01/2023 13:16:21 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleId]    Script Date: 24/01/2023 13:16:21 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 24/01/2023 13:16:21 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 24/01/2023 13:16:21 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_fk_Pessoa_Id]    Script Date: 24/01/2023 13:16:21 ******/
CREATE NONCLUSTERED INDEX [IX_fk_Pessoa_Id] ON [dbo].[Carro]
(
	[fk_Pessoa_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fk_Oficina_Id]    Script Date: 24/01/2023 13:16:21 ******/
CREATE NONCLUSTERED INDEX [IX_Fk_Oficina_Id] ON [dbo].[Imagem]
(
	[Fk_Oficina_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fk_Orçamento_Id]    Script Date: 24/01/2023 13:16:21 ******/
CREATE NONCLUSTERED INDEX [IX_Fk_Orçamento_Id] ON [dbo].[ItemOrçamento]
(
	[Fk_Orçamento_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fk_Oficina_Id]    Script Date: 24/01/2023 13:16:21 ******/
CREATE NONCLUSTERED INDEX [IX_Fk_Oficina_Id] ON [dbo].[Messages]
(
	[Fk_Oficina_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fk_Orçamento_Id]    Script Date: 24/01/2023 13:16:21 ******/
CREATE NONCLUSTERED INDEX [IX_Fk_Orçamento_Id] ON [dbo].[Notificações]
(
	[Fk_Orçamento_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_fk_Carro_Placa]    Script Date: 24/01/2023 13:16:21 ******/
CREATE NONCLUSTERED INDEX [IX_fk_Carro_Placa] ON [dbo].[Orçamento]
(
	[fk_Carro_Placa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_fk_Oficina_Id]    Script Date: 24/01/2023 13:16:21 ******/
CREATE NONCLUSTERED INDEX [IX_fk_Oficina_Id] ON [dbo].[Orçamento]
(
	[fk_Oficina_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_fk_Pessoa_Id]    Script Date: 24/01/2023 13:16:21 ******/
CREATE NONCLUSTERED INDEX [IX_fk_Pessoa_Id] ON [dbo].[Orçamento]
(
	[fk_Pessoa_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fk_Oficina_Id]    Script Date: 24/01/2023 13:16:21 ******/
CREATE NONCLUSTERED INDEX [IX_Fk_Oficina_Id] ON [dbo].[Peça]
(
	[Fk_Oficina_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fk_Categoria_Id]    Script Date: 24/01/2023 13:16:21 ******/
CREATE NONCLUSTERED INDEX [IX_Fk_Categoria_Id] ON [dbo].[Serviço]
(
	[Fk_Categoria_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Fk_Oficina_Id]    Script Date: 24/01/2023 13:16:21 ******/
CREATE NONCLUSTERED INDEX [IX_Fk_Oficina_Id] ON [dbo].[Serviço]
(
	[Fk_Oficina_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Agendamento]  WITH CHECK ADD  CONSTRAINT [FK_Agendamento_Oficina] FOREIGN KEY([Fk_Oficina_Id])
REFERENCES [dbo].[Oficina] ([Id])
GO
ALTER TABLE [dbo].[Agendamento] CHECK CONSTRAINT [FK_Agendamento_Oficina]
GO
ALTER TABLE [dbo].[Agendamento]  WITH CHECK ADD  CONSTRAINT [FK_Agendamento_Orçamento] FOREIGN KEY([Fk_Orçamento_Id])
REFERENCES [dbo].[Orçamento] ([Id])
GO
ALTER TABLE [dbo].[Agendamento] CHECK CONSTRAINT [FK_Agendamento_Orçamento]
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
ALTER TABLE [dbo].[Avaliação]  WITH CHECK ADD  CONSTRAINT [FK_Avaliação_Orçamento] FOREIGN KEY([Fk_Orçamento_Id])
REFERENCES [dbo].[Orçamento] ([Id])
GO
ALTER TABLE [dbo].[Avaliação] CHECK CONSTRAINT [FK_Avaliação_Orçamento]
GO
ALTER TABLE [dbo].[Avaliação]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Avaliação_dbo.Pessoa_fk_Pessoa_Id] FOREIGN KEY([fk_Pessoa_Id])
REFERENCES [dbo].[Pessoa] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Avaliação] CHECK CONSTRAINT [FK_dbo.Avaliação_dbo.Pessoa_fk_Pessoa_Id]
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
ALTER TABLE [dbo].[ItemAvaliação]  WITH CHECK ADD  CONSTRAINT [FK_Avaliação_ItemAvaliação] FOREIGN KEY([Fk_Avaliação_Id])
REFERENCES [dbo].[Avaliação] ([Id])
GO
ALTER TABLE [dbo].[ItemAvaliação] CHECK CONSTRAINT [FK_Avaliação_ItemAvaliação]
GO
ALTER TABLE [dbo].[ItemAvaliação]  WITH CHECK ADD  CONSTRAINT [FK_Serviço_ItemAvaliação] FOREIGN KEY([Fk_Serviço_Id])
REFERENCES [dbo].[Serviço] ([Id])
GO
ALTER TABLE [dbo].[ItemAvaliação] CHECK CONSTRAINT [FK_Serviço_ItemAvaliação]
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
Insert into [dbo].[Categoria] values ('Geometria e Bal.')
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
USE [TBN(IN305)]
GO
SET IDENTITY_INSERT [dbo].[AspNetUsers] ON 
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (2, N'ramos@gmail.com', 0, N'AKMmTICslduMhw0PcVfFyK5JCivYnInabXcQjTvqgrXDBVnH+yRtmFKNeqKeCNBzkA==', N'39484013-db14-4c10-9ae5-0ac5608d4884', NULL, 0, 0, NULL, 0, 0, N'ramos@gmail.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (3, N'Martins@gmail.com', 0, N'AJPgEBhS+8qMIOmosV7fMtTIJTKLBjjPRc2/GMNKcaH8Wd1qNv1ypxQGqVv1tSSqEg==', N'bf94f993-87bb-4a3f-b544-15bcc34247e8', NULL, 0, 0, NULL, 0, 0, N'Martins@gmail.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (4, N'racer@gmail.com', 0, N'AMsaia61sOf326w8Wx8JV8JXotEFneenTC5MfAoq1g+kiKb6yQL091WgbKfOkDrxBw==', N'3a4e779b-809e-4cad-ad1f-267a35dcc644', NULL, 0, 0, NULL, 0, 0, N'racer@gmail.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (5, N'Pitstop@gmail.com', 0, N'AE5pfzD0cu+vr8zqdu7L8RwwPsLypOYlKLJQGDQxtg3LzPoQzA9lMEKV7VEcvDiRug==', N'b30ae86d-370c-40ad-b586-645caeef1526', NULL, 0, 0, NULL, 0, 0, N'Pitstop@gmail.com')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (6, N'paulo@gmail.com', 0, N'AGYGTUTP1v0GMlwAT06lJAAoZlSZWdHEzzOxs+geX+w+1S8bKVOfRPdsOyTq/HtsTQ==', N'2d01e708-1359-41a3-a363-27980af519de', NULL, 0, 0, NULL, 0, 0, N'paulo@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[AspNetUsers] OFF
GO
INSERT [dbo].[Pessoa] ([Id], [Nome], [Sobrenome], [CEP], [Estado], [Cidade], [Rua], [Número], [Complemento], [Email], [CPF], [CNPJ], [Pessoa_TIPO]) VALUES (6, N'Paulo Victor', N'Gomes', N'20730440', N'RJ', N'Rio de Janeiro', N'Rua Venâncio Ribeiro', 161, N'104', N'paulo@gmail.com', N'20589780797', NULL, 1)
GO
INSERT [dbo].[Carro] ([Placa], [Cor], [Modelo], [Motorização], [Marca], [fk_Pessoa_Id]) VALUES (N'ABC1D23', N'Branco', N'Focus', N'1.0', N'Ford', 6)
GO
INSERT [dbo].[Oficina] ([Id], [Email], [CNPJ], [Nome], [CEP], [Estado], [Cidade], [Rua], [Número], [Complemento], [Bairro], [Descrição], [Aprovada], [AceitaImportado], [Finalizada], [HorarioFuncionamento], [DiasFuncionamento]) VALUES (2, N'ramos@gmail.com', N'22714243000124', N'Oficinas Ramos', N'25086-300', N'RJ', N'Duque de Caxias', N'Rua Elodir Freitas', 200, NULL, N'Parque Redentor', N'Aqui nas Oficinas Ramos nós efetuamos serviços de qualidade, com tempos de resposta muito mais rápidos e preços muito mais acessíveis que a competição, venha pra Ramos agora mesmo!', 1, 1, 0, N'08:00/18:00', N',Segunda,Terça,Quarta,Quinta,Sexta,Sábado')
GO
INSERT [dbo].[Oficina] ([Id], [Email], [CNPJ], [Nome], [CEP], [Estado], [Cidade], [Rua], [Número], [Complemento], [Bairro], [Descrição], [Aprovada], [AceitaImportado], [Finalizada], [HorarioFuncionamento], [DiasFuncionamento]) VALUES (3, N'Martins@gmail.com', N'88652841000124', N'Martins Car', N'21850-270', N'RJ', N'Rio de Janeiro', N'Rua Hiroati Torigoi', 325, N'Loja A', N'Bangu', N'A Martins Car está estabelecida no mercado faz 20 anos. Possuimos vasto conhecimento nas áreas nas quais operamos. Faça um orçamento gratuito conosco!', 1, 0, 0, N'09:00/18:00', N',Segunda,Terça,Quarta,Quinta,Sexta')
GO
INSERT [dbo].[Oficina] ([Id], [Email], [CNPJ], [Nome], [CEP], [Estado], [Cidade], [Rua], [Número], [Complemento], [Bairro], [Descrição], [Aprovada], [AceitaImportado], [Finalizada], [HorarioFuncionamento], [DiasFuncionamento]) VALUES (4, N'racer@gmail.com', N'77749157000149', N'Speed Racer', N'22440-970', N'RJ', N'Rio de Janeiro', N'Avenida Ataulfo de Paiva', 543, N'Loja B', N'Leblon', N'Fazemos reparos com a maior qualidade no menor tempo possível!', 1, 1, 0, N'10:00/20:00', N',Segunda,Terça,Quarta,Quinta,Sexta,Sábado')
GO
INSERT [dbo].[Oficina] ([Id], [Email], [CNPJ], [Nome], [CEP], [Estado], [Cidade], [Rua], [Número], [Complemento], [Bairro], [Descrição], [Aprovada], [AceitaImportado], [Finalizada], [HorarioFuncionamento], [DiasFuncionamento]) VALUES (5, N'Pitstop@gmail.com', N'36502701000176', N'Pitstop Oficinas', N'22621-010', N'RJ', N'Rio de Janeiro', N'Rua Alessio Venturi', 222, NULL, N'Barra da Tijuca', N'Experimente o que há de melhor em serviços automotivos!', 1, 0, 0, N'09:20/19:40', N',Segunda,Terça,Quarta,Quinta')
GO
SET IDENTITY_INSERT [dbo].[Orçamento] ON 
GO
INSERT [dbo].[Orçamento] ([Id], [Valor], [Status], [fk_Pessoa_Id], [fk_Oficina_Id], [fk_Carro_Placa], [Data_Orçamento], [Tipo]) VALUES (1, 800.0000, 32, 6, 3, N'ABC1D23', CAST(N'2023-02-06T22:21:06.117' AS DateTime), 2)
GO
SET IDENTITY_INSERT [dbo].[Orçamento] OFF
GO
SET IDENTITY_INSERT [dbo].[Avaliação] ON 
GO
INSERT [dbo].[Avaliação] ([Id], [Estrelas], [Texto], [fk_Pessoa_Id], [Fk_Orçamento_Id], [Fk_Oficina_Id]) VALUES (1, 5, N'Ótima experiência. Atendimento rápido e genuíno interesse em ajudar o consumidor, 10/10, show!', 6, 1, 3)
GO
SET IDENTITY_INSERT [dbo].[Avaliação] OFF
GO
SET IDENTITY_INSERT [dbo].[Serviço] ON 
GO
INSERT [dbo].[Serviço] ([Id], [Fk_Oficina_Id], [Fk_Categoria_Id], [Nome], [Descrição], [PreçoMin], [PreçoMax]) VALUES (1, 4, 1, N'Troca de óleo de motor e filtro', N'A principal função do óleo do motor é a redução do atrito entre as partes móveis através de uma fina película lubrificante. Recomendamos a troca a cada 10.000 km ou 6 meses.', 120.0000, 400.0000)
GO
INSERT [dbo].[Serviço] ([Id], [Fk_Oficina_Id], [Fk_Categoria_Id], [Nome], [Descrição], [PreçoMin], [PreçoMax]) VALUES (2, 4, 1, N'Revisão Preventiva', N'Revisamos diversos tipos de veículos. A revisão preventiva faz com que seu veículo seja eficiente e seguro. Consultar manual da fabricante para saber o intervalo recomendado.', NULL, NULL)
GO
INSERT [dbo].[Serviço] ([Id], [Fk_Oficina_Id], [Fk_Categoria_Id], [Nome], [Descrição], [PreçoMin], [PreçoMax]) VALUES (3, 4, 2, N'Reparo do sistema de direção hidráulica', N'O sistema de direção hidráulica serve para facilitar o esforço do motorista ao manobrar o veículo. Agende uma avaliação para que possamos dar uma olhada e resolver seu problema!', NULL, NULL)
GO
INSERT [dbo].[Serviço] ([Id], [Fk_Oficina_Id], [Fk_Categoria_Id], [Nome], [Descrição], [PreçoMin], [PreçoMax]) VALUES (4, 4, 1, N'Balanceamento de Pneus', N'O balanceamento faz com que as rodas girem sem provocar vibrações ao dirigir o veículo. O balanceamento é uma manutenção de rotina, atente-se a especificação que consta no manual do veículo e venha agendar uma análise conosco!', NULL, NULL)
GO
INSERT [dbo].[Serviço] ([Id], [Fk_Oficina_Id], [Fk_Categoria_Id], [Nome], [Descrição], [PreçoMin], [PreçoMax]) VALUES (5, 3, 3, N'Troca de Amortecedores', N'Se o seu carro apresenta perda de estabilidade ao dirigir, desgaste acentuado dos pneus, barulho vindo da suspensão ou presença de óleo no amortecedor, venha agendar uma análise conosco!', 300.0000, 800.0000)
GO
INSERT [dbo].[Serviço] ([Id], [Fk_Oficina_Id], [Fk_Categoria_Id], [Nome], [Descrição], [PreçoMin], [PreçoMax]) VALUES (6, 3, 3, N'Revisão de Amortecedor', N'O bom estado do amortecedor é essencial para manter a estabilidade do veículo e evitar diversos problemas. 
Consulte o manual do veículo para saber a frequência na qual a revisão deve ser feita.', NULL, NULL)
GO
INSERT [dbo].[Serviço] ([Id], [Fk_Oficina_Id], [Fk_Categoria_Id], [Nome], [Descrição], [PreçoMin], [PreçoMax]) VALUES (7, 3, 3, N'Revisão da Suspensão', N'O bom estado da suspensão é essencial para manter a estabilidade do veículo e evitar diversos problemas. Consulte o manual do veículo para saber a frequência na qual a revisão deve ser feita.', NULL, NULL)
GO
INSERT [dbo].[Serviço] ([Id], [Fk_Oficina_Id], [Fk_Categoria_Id], [Nome], [Descrição], [PreçoMin], [PreçoMax]) VALUES (8, 2, 7, N'Troca de Correia Dentada', N'Se o carro apresenta dificuldade para trocar a marcha, ruídos estranhos ou superaquecimento do motor, pode ser que seja a hora da troca da Correia Dentada.', 150.0000, 450.0000)
GO
INSERT [dbo].[Serviço] ([Id], [Fk_Oficina_Id], [Fk_Categoria_Id], [Nome], [Descrição], [PreçoMin], [PreçoMax]) VALUES (9, 2, 7, N'Revisão de Motor', N'O motor é um das partes importantes do veículo e é composto de diversos itens que necessitam de revisão periódica. Agende agora uma revisão conosco!', NULL, NULL)
GO
INSERT [dbo].[Serviço] ([Id], [Fk_Oficina_Id], [Fk_Categoria_Id], [Nome], [Descrição], [PreçoMin], [PreçoMax]) VALUES (10, 2, 7, N'Troca de Velas e Cabos de Ignição', NULL, NULL, NULL)
GO
INSERT [dbo].[Serviço] ([Id], [Fk_Oficina_Id], [Fk_Categoria_Id], [Nome], [Descrição], [PreçoMin], [PreçoMax]) VALUES (11, 2, 7, N'Troca de Filtro de Combustível', N'A troca do filtro de combustível, conforme a especificação do fabricante, normalmente ocorre a cada 10.000 KM, troque agora o filtro conosco!', NULL, NULL)
GO
INSERT [dbo].[Serviço] ([Id], [Fk_Oficina_Id], [Fk_Categoria_Id], [Nome], [Descrição], [PreçoMin], [PreçoMax]) VALUES (12, 5, 8, N'Troca do Filtro do Ar Condicionado', N'A troca do filtro de ar-condicionado é importante para melhorar a qualidade do ar do seu veículo. ', 50.0000, NULL)
GO
INSERT [dbo].[Serviço] ([Id], [Fk_Oficina_Id], [Fk_Categoria_Id], [Nome], [Descrição], [PreçoMin], [PreçoMax]) VALUES (13, 5, 9, N'Revisão de Embragem', N'O sistema de transmissão do carro ameniza os desgastes dos componentes internos e do motor. O veículo precisa passar por revisões periódicas de acordo com as especificações do fabricante. Agende agora sua análise!', NULL, NULL)
GO
INSERT [dbo].[Serviço] ([Id], [Fk_Oficina_Id], [Fk_Categoria_Id], [Nome], [Descrição], [PreçoMin], [PreçoMax]) VALUES (14, 5, 10, N'Martelinho de Ouro', N'Bateu?, Amassou? O Martelinho de Ouro é uma técnica que pode resolver seus problemas! É uma opção simples e barata para que se evite a repintura do veículo e para baratear os custos.', NULL, NULL)
GO
INSERT [dbo].[Serviço] ([Id], [Fk_Oficina_Id], [Fk_Categoria_Id], [Nome], [Descrição], [PreçoMin], [PreçoMax]) VALUES (15, 5, 11, N'Revisão de Escapamento', N'O sistema de escapamento do veículo tem como principal função a eliminação dos gases que são gerados na queima do combustível, além de reduzir o barulho do motor. ', NULL, NULL)
GO
INSERT [dbo].[Serviço] ([Id], [Fk_Oficina_Id], [Fk_Categoria_Id], [Nome], [Descrição], [PreçoMin], [PreçoMax]) VALUES (16, 4, 4, N'Troca de Pneu', N'Trocamos pneus Aro 13 até Aro 18.', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Serviço] OFF
GO
SET IDENTITY_INSERT [dbo].[ItemAvaliação] ON 
GO
INSERT [dbo].[ItemAvaliação] ([Id], [Estrelas], [Fk_Avaliação_Id], [Fk_Serviço_Id]) VALUES (1, 5, 1, 6)
GO
INSERT [dbo].[ItemAvaliação] ([Id], [Estrelas], [Fk_Avaliação_Id], [Fk_Serviço_Id]) VALUES (2, 5, 1, 5)
GO
SET IDENTITY_INSERT [dbo].[ItemAvaliação] OFF
GO
SET IDENTITY_INSERT [dbo].[Agendamento] ON 
GO
INSERT [dbo].[Agendamento] ([Id], [Fk_Oficina_Id], [Fk_Pessoa_Id], [Fk_Orçamento_Id], [Data], [Finalizado]) VALUES (1, 3, 6, 1, CAST(N'2023-02-15T13:00:00.000' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Agendamento] OFF
GO
INSERT [dbo].[Imagem] ([Fk_Oficina_Id], [Url]) VALUES (2, N'../../UploadedFiles\638113071180566736Ramos.png')
GO
INSERT [dbo].[Imagem] ([Fk_Oficina_Id], [Url]) VALUES (3, N'../../UploadedFiles\638113076091908957MartinsCar.png')
GO
INSERT [dbo].[Imagem] ([Fk_Oficina_Id], [Url]) VALUES (4, N'../../UploadedFiles\638113078548664284Speed Racer.png')
GO
INSERT [dbo].[Imagem] ([Fk_Oficina_Id], [Url]) VALUES (5, N'../../UploadedFiles\638113081222779368Pitstop.png')
GO
SET IDENTITY_INSERT [dbo].[Peça] ON 
GO
INSERT [dbo].[Peça] ([Id], [Nome], [Fk_Oficina_Id], [PreçoMin], [PreçoMax], [Marca], [Descrição], [Código]) VALUES (1, N'Filtro Ar Condicionado Citroën Aircross 1.6', 5, 20.0000, 35.0000, N'Citroen', N'Filtro de Ar Condicionado para veículos Citroen de modelo Aircross de 2011 a 2021.', N'AKX1446')
GO
INSERT [dbo].[Peça] ([Id], [Nome], [Fk_Oficina_Id], [PreçoMin], [PreçoMax], [Marca], [Descrição], [Código]) VALUES (2, N'Filtro Ar Condicionado Corolla 1.6', 5, 17.0000, 25.0000, N'Toyota', N'Filtro de Ar Condicionado para veículos Toyota de modelo Corolla.', N' AKX1963')
GO
INSERT [dbo].[Peça] ([Id], [Nome], [Fk_Oficina_Id], [PreçoMin], [PreçoMax], [Marca], [Descrição], [Código]) VALUES (3, N'Filtro Ar Condicionado Ford Focus 1.6 2.0', 5, 35.0000, 45.0000, N'Ford', N'Filtro de Ar Condicionado para veículos Ford de modelo Focus de 2014 a 2019.', N'AKX3594F')
GO
INSERT [dbo].[Peça] ([Id], [Nome], [Fk_Oficina_Id], [PreçoMin], [PreçoMax], [Marca], [Descrição], [Código]) VALUES (4, N'Filtro Ar Condicionado Honda Civic 1.8', 5, 20.0000, 35.0000, N'Honda', N'Filtro de Ar Condicionado para veículos Honda de modelo Civic de 2006 a 2015.', N'AKX1456')
GO
INSERT [dbo].[Peça] ([Id], [Nome], [Fk_Oficina_Id], [PreçoMin], [PreçoMax], [Marca], [Descrição], [Código]) VALUES (5, N'Filtro de Ar Condicionado', 5, NULL, NULL, N'', N'', N'')
GO
INSERT [dbo].[Peça] ([Id], [Nome], [Fk_Oficina_Id], [PreçoMin], [PreçoMax], [Marca], [Descrição], [Código]) VALUES (6, N'MOBIL SUPER 5W-30 SINTÉTICO 1L', 4, 30.0000, 45.0000, N'MOBIL', N'Óleo sintético para motor que proporciona excelente proteção para veículos novos e antigos, ajudando a aumentar a vida útil do motor, mesmo em condições severas de operação.
', NULL)
GO
INSERT [dbo].[Peça] ([Id], [Nome], [Fk_Oficina_Id], [PreçoMin], [PreçoMax], [Marca], [Descrição], [Código]) VALUES (7, N'Mobil Super Original Mineral 20W-50 1L', 4, 25.0000, 33.0000, N'MOBIL', N'Óleo mineral multiviscoso de última geração especialmente formulado para proteção em condições severas como trânsito intenso.', N'')
GO
INSERT [dbo].[Peça] ([Id], [Nome], [Fk_Oficina_Id], [PreçoMin], [PreçoMax], [Marca], [Descrição], [Código]) VALUES (8, N'PNEU 185/60 R15 88H XL TL PRIMACY 4 MICHELIN', 4, 680.0000, 850.0000, N'Michelin', N'PNEU 185/60 R15 88H XL TL PRIMACY 4 MICHELIN, PNEU 185/60 R15 88H XL TL PRIMACY 4 MICHELIN', N'49238 - MICHELIN-305966')
GO
INSERT [dbo].[Peça] ([Id], [Nome], [Fk_Oficina_Id], [PreçoMin], [PreçoMax], [Marca], [Descrição], [Código]) VALUES (9, N'PNEU 205/55 R17 95V XL PRIMACY 4 MICHELIN', 4, 970.0000, 1400.0000, N'MICHELIN', N'O PNEU MICHELIN PRIMACY 4 TRAZ A MELHOR TECNOLOGIA PARA A MAIOR SEGURANÇA NA FRENAGEM, DURABILIDADE E PERFORMANCE DO PNEU NO PISO MOLHADO.', N'64028 - MICHELIN-305985')
GO
INSERT [dbo].[Peça] ([Id], [Nome], [Fk_Oficina_Id], [PreçoMin], [PreçoMax], [Marca], [Descrição], [Código]) VALUES (10, N'PNEU 175/65 R14 82H ENERGY XM2 MICHELIN', 4, 650.0000, 900.0000, N'MICHELIN', N'O PNEU MICHELIN ENERGY XM2+ DURA MAIS, É MAIS RESISTENTE E OFERECE UMA EXCELENTE PERFORMANCE DE FRENAGEM EM PISO MOLHADO, GARANTINDO SEGURANÇA EM PRIMEIRO LUGAR ATÉ O FINAL DA VIDA ÚTIL DO PNEU.', N'62185 - MICHELIN-305952')
GO
INSERT [dbo].[Peça] ([Id], [Nome], [Fk_Oficina_Id], [PreçoMin], [PreçoMax], [Marca], [Descrição], [Código]) VALUES (11, N'Tensionador Correia Dentada Corsa', 2, 130.0000, 180.0000, N'SKF', N'Tensionador Correia Dentada Corsa 2006 a 2012', N'VKM15402L')
GO
INSERT [dbo].[Peça] ([Id], [Nome], [Fk_Oficina_Id], [PreçoMin], [PreçoMax], [Marca], [Descrição], [Código]) VALUES (12, N'Correia Dentada Fox 1.0', 2, 50.0000, 90.0000, N'DAYCO', N'Correia Dentada Fox 1.0 2004 a 2008', N'135STP8M190H')
GO
INSERT [dbo].[Peça] ([Id], [Nome], [Fk_Oficina_Id], [PreçoMin], [PreçoMax], [Marca], [Descrição], [Código]) VALUES (13, N'Correia Dentada Siena', 2, 48.0000, 90.0000, N'DAYCO', N'Correia Dentada Siena 1.0 1.3 1.5 1.6 1.8 1998 a 2000 Dayco', N'135SHPN170H')
GO
INSERT [dbo].[Peça] ([Id], [Nome], [Fk_Oficina_Id], [PreçoMin], [PreçoMax], [Marca], [Descrição], [Código]) VALUES (14, N'Vela de Ignição dr8ea-n', 2, 13.0000, 20.0000, N'COBREQ', N'', N'dr8ea-n')
GO
INSERT [dbo].[Peça] ([Id], [Nome], [Fk_Oficina_Id], [PreçoMin], [PreçoMax], [Marca], [Descrição], [Código]) VALUES (15, N'Vela de Ignição GV7R44', 2, 16.9000, 25.0000, N'GAUSS', N'Vela de Ignição GV7R44 para Hyundai HB20 HB20s e Kia Picanto', N'GV7R44')
GO
INSERT [dbo].[Peça] ([Id], [Nome], [Fk_Oficina_Id], [PreçoMin], [PreçoMax], [Marca], [Descrição], [Código]) VALUES (16, N'Cabo de Vela de Ignição St-V08', 2, 107.0000, 210.0000, N'COBREQ', N'Também conhecido como cabo de ignição. Responsável por distribuir a energia que a bobina fornece, levando-a até as velas de ignição.', N'')
GO
SET IDENTITY_INSERT [dbo].[Peça] OFF
GO
SET IDENTITY_INSERT [dbo].[ItemOrçamento] ON 
GO
INSERT [dbo].[ItemOrçamento] ([Id], [Fk_Orçamento_Id], [Nome], [Preço], [Descrição], [Quantidade], [Avaliado], [Tipo]) VALUES (1, 1, N'Revisão de Amortecedor', 200.0000, N'O bom estado do amortecedor é essencial para manter a estabilidade do veículo e evitar diversos problemas. 
Consulte o manual do veículo para saber a frequência na qual a revisão deve ser feita.', 1, 0, 1)
GO
INSERT [dbo].[ItemOrçamento] ([Id], [Fk_Orçamento_Id], [Nome], [Preço], [Descrição], [Quantidade], [Avaliado], [Tipo]) VALUES (2, 1, N'Troca de Amortecedores', 600.0000, N'Se o seu carro apresenta perda de estabilidade ao dirigir, desgaste acentuado dos pneus, barulho vindo da suspensão ou presença de óleo no amortecedor, venha agendar uma análise conosco!', 1, 0, 1)
GO
SET IDENTITY_INSERT [dbo].[ItemOrçamento] OFF
GO
SET IDENTITY_INSERT [dbo].[Notificações] ON 
GO
INSERT [dbo].[Notificações] ([Id], [Texto], [Fk_Orçamento_Id], [Fk_User_Id], [Lido]) VALUES (1, N'Nova solicitação de Orçamento!', 1, 3, 1)
GO
INSERT [dbo].[Notificações] ([Id], [Texto], [Fk_Orçamento_Id], [Fk_User_Id], [Lido]) VALUES (2, N'Solicitação de orçamento aprovada pela oficina!', 1, 6, 1)
GO
INSERT [dbo].[Notificações] ([Id], [Texto], [Fk_Orçamento_Id], [Fk_User_Id], [Lido]) VALUES (3, N'Agendamento Aprovado. Leve o veículo até a Oficina na data agendada', 1, 6, 1)
GO
INSERT [dbo].[Notificações] ([Id], [Texto], [Fk_Orçamento_Id], [Fk_User_Id], [Lido]) VALUES (4, N'Avaliação do Veículo confirmada!', 1, 6, 1)
GO
INSERT [dbo].[Notificações] ([Id], [Texto], [Fk_Orçamento_Id], [Fk_User_Id], [Lido]) VALUES (5, N'Orçamento finalizado, agora só falta aprovar!', 1, 6, 1)
GO
INSERT [dbo].[Notificações] ([Id], [Texto], [Fk_Orçamento_Id], [Fk_User_Id], [Lido]) VALUES (6, N'Cliente aprovou o Orçamento!', 1, 3, 0)
GO
SET IDENTITY_INSERT [dbo].[Notificações] OFF
GO
SET IDENTITY_INSERT [dbo].[CelularTelefone] ON 
GO
INSERT [dbo].[CelularTelefone] ([Id], [CelularTelefone1], [Fk_User_Id]) VALUES (1, N'21979313814', 2)
GO
INSERT [dbo].[CelularTelefone] ([Id], [CelularTelefone1], [Fk_User_Id]) VALUES (2, N'2199991111', 3)
GO
INSERT [dbo].[CelularTelefone] ([Id], [CelularTelefone1], [Fk_User_Id]) VALUES (3, N'21966666666', 4)
GO
INSERT [dbo].[CelularTelefone] ([Id], [CelularTelefone1], [Fk_User_Id]) VALUES (4, N'2196443322', 5)
GO
INSERT [dbo].[CelularTelefone] ([Id], [CelularTelefone1], [Fk_User_Id]) VALUES (5, N'21983178951', 6)
GO
SET IDENTITY_INSERT [dbo].[CelularTelefone] OFF
GO
