USE [master]
GO
/****** Object:  Database [TBN(IN305)]    Script Date: 18/11/2022 11:01:15 ******/
CREATE DATABASE [TBN(IN305)]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TBN(IN305)', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TBN(IN305).mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TBN(IN305)_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TBN(IN305)_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
ALTER DATABASE [TBN(IN305)] SET AUTO_CLOSE OFF 
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
ALTER DATABASE [TBN(IN305)] SET  DISABLE_BROKER 
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
ALTER DATABASE [TBN(IN305)] SET READ_COMMITTED_SNAPSHOT OFF 
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
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 18/11/2022 11:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Administrador]    Script Date: 18/11/2022 11:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrador](
	[Id_Administrativo] [int] IDENTITY(1,1) NOT NULL,
	[fk_Oficina_Id] [int] NOT NULL,
 CONSTRAINT [PK__Administ__D57A3A0AC25C4BBD] PRIMARY KEY CLUSTERED 
(
	[Id_Administrativo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 18/11/2022 11:01:15 ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 18/11/2022 11:01:15 ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 18/11/2022 11:01:15 ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 18/11/2022 11:01:15 ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 18/11/2022 11:01:15 ******/
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
/****** Object:  Table [dbo].[Avalia��o]    Script Date: 18/11/2022 11:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Avalia��o](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Estrelas] [int] NOT NULL,
	[Texto] [nvarchar](250) NULL,
	[fk_Servi�os_Id] [int] NOT NULL,
	[fk_Pessoa_Id] [int] NOT NULL,
 CONSTRAINT [PK__Avalia��__3214EC0790A8D09E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carro]    Script Date: 18/11/2022 11:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carro](
	[Placa] [nvarchar](7) NOT NULL,
	[Cor] [nvarchar](15) NOT NULL,
	[Modelo] [nvarchar](25) NOT NULL,
	[Motoriza��o] [nvarchar](3) NOT NULL,
	[Marca] [nvarchar](15) NOT NULL,
	[fk_Pessoa_Id] [int] NOT NULL,
 CONSTRAINT [PK__Carro__8310F99CC5D2A0FE] PRIMARY KEY CLUSTERED 
(
	[Placa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CelularTelefone]    Script Date: 18/11/2022 11:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CelularTelefone](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CelularTelefone1] [nvarchar](11) NOT NULL,
	[Fk_User_Id] [int] NOT NULL,
 CONSTRAINT [PK__CelularT__3213E83F90E65F25] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Imagem]    Script Date: 18/11/2022 11:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Imagem](
	[Url] [nvarchar](200) NOT NULL,
	[Fk_Oficina_Id] [int] NOT NULL,
 CONSTRAINT [PK_Imagem] PRIMARY KEY CLUSTERED 
(
	[Fk_Oficina_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemOr�amento]    Script Date: 18/11/2022 11:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemOr�amento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fk_Or�amento_Id] [int] NOT NULL,
	[Fk_Pe�a_Id] [int] NULL,
	[Fk_Servi�o_Id] [int] NULL,
	[Quantidade] [float] NULL,
	[Descri��o] [nvarchar](max) NULL,
 CONSTRAINT [PK_ItemOr�amento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC,
	[Fk_Or�amento_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Oficina]    Script Date: 18/11/2022 11:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Oficina](
	[Id] [int] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[CNPJ] [nvarchar](14) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Estado] [nvarchar](25) NOT NULL,
	[Cidade] [nvarchar](30) NOT NULL,
	[Rua] [nvarchar](50) NOT NULL,
	[N�mero] [int] NOT NULL,
	[Complemento] [nvarchar](20) NULL,
 CONSTRAINT [PK_Oficina_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Or�amento]    Script Date: 18/11/2022 11:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Or�amento](
	[Valor] [money] NOT NULL,
	[Status] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[fk_Pessoa_Id] [int] NOT NULL,
	[fk_Oficina_Id] [int] NOT NULL,
	[fk_Carro_Placa] [nvarchar](7) NOT NULL,
	[Data_Or�amento] [datetime] NOT NULL,
	[Data_Aprova��o] [datetime] NULL,
 CONSTRAINT [PK__Or�ament__78BB6B8C169CF93E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pe�a]    Script Date: 18/11/2022 11:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pe�a](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Fk_Oficina_Id] [int] NOT NULL,
	[Pre�o] [money] NOT NULL,
	[Marca] [nvarchar](50) NULL,
	[Descri��o] [nvarchar](max) NULL,
	[C�digo] [nvarchar](max) NULL,
 CONSTRAINT [PK__Pe�a__3214EC07766891AF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pessoa]    Script Date: 18/11/2022 11:01:15 ******/
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
	[N�mero] [int] NOT NULL,
	[Complemento] [nvarchar](30) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[CPF] [nvarchar](11) NULL,
	[CNPJ] [nvarchar](14) NULL,
	[Pessoa_TIPO] [int] NOT NULL,
 CONSTRAINT [PK__Pessoa__3214EC07473BCC7C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Servi�o]    Script Date: 18/11/2022 11:01:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servi�o](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Fk_Oficina_Id] [int] NOT NULL,
	[Descri��o] [nvarchar](max) NULL,
	[Pre�o] [money] NOT NULL,
 CONSTRAINT [PK_Servi�o] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 18/11/2022 11:01:15 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 18/11/2022 11:01:15 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 18/11/2022 11:01:15 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleId]    Script Date: 18/11/2022 11:01:15 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserId]    Script Date: 18/11/2022 11:01:15 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 18/11/2022 11:01:15 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__PESSOA__CNPJ]    Script Date: 18/11/2022 11:01:15 ******/
CREATE NONCLUSTERED INDEX [UQ__PESSOA__CNPJ] ON [dbo].[Pessoa]
(
	[CNPJ] ASC
)
WHERE ([CNPJ] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__PESSOA__CPF]    Script Date: 18/11/2022 11:01:15 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ__PESSOA__CPF] ON [dbo].[Pessoa]
(
	[CPF] ASC
)
WHERE ([CPF] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
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
ALTER TABLE [dbo].[Avalia��o]  WITH CHECK ADD  CONSTRAINT [FK_Avalia��o_3] FOREIGN KEY([fk_Pessoa_Id])
REFERENCES [dbo].[Pessoa] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Avalia��o] CHECK CONSTRAINT [FK_Avalia��o_3]
GO
ALTER TABLE [dbo].[Avalia��o]  WITH CHECK ADD  CONSTRAINT [FK_Avalia��o_Servi�o] FOREIGN KEY([fk_Servi�os_Id])
REFERENCES [dbo].[Servi�o] ([Id])
GO
ALTER TABLE [dbo].[Avalia��o] CHECK CONSTRAINT [FK_Avalia��o_Servi�o]
GO
ALTER TABLE [dbo].[Carro]  WITH CHECK ADD  CONSTRAINT [FK_Carro_2] FOREIGN KEY([fk_Pessoa_Id])
REFERENCES [dbo].[Pessoa] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Carro] CHECK CONSTRAINT [FK_Carro_2]
GO
ALTER TABLE [dbo].[CelularTelefone]  WITH CHECK ADD  CONSTRAINT [FK_CelularTelefone_Oficina] FOREIGN KEY([Fk_User_Id])
REFERENCES [dbo].[Oficina] ([Id])
GO
ALTER TABLE [dbo].[CelularTelefone] CHECK CONSTRAINT [FK_CelularTelefone_Oficina]
GO
ALTER TABLE [dbo].[CelularTelefone]  WITH CHECK ADD  CONSTRAINT [FK_CelularTelefone_Pessoa] FOREIGN KEY([Fk_User_Id])
REFERENCES [dbo].[Pessoa] ([Id])
GO
ALTER TABLE [dbo].[CelularTelefone] CHECK CONSTRAINT [FK_CelularTelefone_Pessoa]
GO
ALTER TABLE [dbo].[Imagem]  WITH CHECK ADD  CONSTRAINT [Fk_Oficina_Id] FOREIGN KEY([Fk_Oficina_Id])
REFERENCES [dbo].[Oficina] ([Id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Imagem] CHECK CONSTRAINT [Fk_Oficina_Id]
GO
ALTER TABLE [dbo].[ItemOr�amento]  WITH CHECK ADD  CONSTRAINT [FK_Or�amento_ItemOr�amento] FOREIGN KEY([Fk_Or�amento_Id])
REFERENCES [dbo].[Or�amento] ([Id])
GO
ALTER TABLE [dbo].[ItemOr�amento] CHECK CONSTRAINT [FK_Or�amento_ItemOr�amento]
GO
ALTER TABLE [dbo].[ItemOr�amento]  WITH CHECK ADD  CONSTRAINT [FK_Pe�a_ItemOr�amento] FOREIGN KEY([Fk_Pe�a_Id])
REFERENCES [dbo].[Pe�a] ([Id])
GO
ALTER TABLE [dbo].[ItemOr�amento] CHECK CONSTRAINT [FK_Pe�a_ItemOr�amento]
GO
ALTER TABLE [dbo].[ItemOr�amento]  WITH CHECK ADD  CONSTRAINT [FK_Servi�o_ItemOr�amento] FOREIGN KEY([Fk_Servi�o_Id])
REFERENCES [dbo].[Servi�o] ([Id])
GO
ALTER TABLE [dbo].[ItemOr�amento] CHECK CONSTRAINT [FK_Servi�o_ItemOr�amento]
GO
ALTER TABLE [dbo].[Or�amento]  WITH CHECK ADD  CONSTRAINT [FK_Or�amento_Carro] FOREIGN KEY([fk_Carro_Placa])
REFERENCES [dbo].[Carro] ([Placa])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Or�amento] CHECK CONSTRAINT [FK_Or�amento_Carro]
GO
ALTER TABLE [dbo].[Or�amento]  WITH CHECK ADD  CONSTRAINT [FK_Or�amento_Oficina] FOREIGN KEY([fk_Oficina_Id])
REFERENCES [dbo].[Oficina] ([Id])
GO
ALTER TABLE [dbo].[Or�amento] CHECK CONSTRAINT [FK_Or�amento_Oficina]
GO
ALTER TABLE [dbo].[Or�amento]  WITH CHECK ADD  CONSTRAINT [FK_Or�amento_Pessoa] FOREIGN KEY([fk_Pessoa_Id])
REFERENCES [dbo].[Pessoa] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Or�amento] CHECK CONSTRAINT [FK_Or�amento_Pessoa]
GO
ALTER TABLE [dbo].[Pe�a]  WITH CHECK ADD  CONSTRAINT [FK_Pe�a_Oficina] FOREIGN KEY([Fk_Oficina_Id])
REFERENCES [dbo].[Oficina] ([Id])
GO
ALTER TABLE [dbo].[Pe�a] CHECK CONSTRAINT [FK_Pe�a_Oficina]
GO
ALTER TABLE [dbo].[Servi�o]  WITH CHECK ADD  CONSTRAINT [FK_Servi�o_Oficina] FOREIGN KEY([Fk_Oficina_Id])
REFERENCES [dbo].[Oficina] ([Id])
GO
ALTER TABLE [dbo].[Servi�o] CHECK CONSTRAINT [FK_Servi�o_Oficina]
GO
USE [master]
GO
ALTER DATABASE [TBN(IN305)] SET  READ_WRITE 
GO
