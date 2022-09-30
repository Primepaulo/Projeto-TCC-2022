/* TCC_Lógico_5_ATUALIZADO (1): */

CREATE TABLE Orçamento (
    Valor money NOT NULL,
    Status int NOT NULL,
    Id_Orçamento int PRIMARY KEY,
    fk_Pessoa_Id int,
    fk_Oficina_CNPJ nvarchar(14),
    fk_Carro_Placa nvarchar(7)
);

CREATE TABLE Oficina (
    Email nvarchar(50) NOT NULL,
    CNPJ nvarchar(14) PRIMARY KEY,
    Nome nvarchar(50) NOT NULL,
    fk_TelefoneCelular_Id int,
    Estado nvarchar(25) NOT NULL,
    Cidade nvarchar(30) NOT NULL,
    Rua nvarchar(50) NOT NULL,
    Número int NOT NULL,
    Complemento nvarchar(20)
);

CREATE TABLE Carro (
    Placa nvarchar(7) PRIMARY KEY,
    Cor nvarchar(15) NOT NULL,
    Modelo nvarchar(25) NOT NULL,
    Motorização decimal(2,1) NOT NULL,
    Marca nvarchar(15) NOT NULL,
    fk_Pessoa_Id int
);

CREATE TABLE Peça (
    Id int PRIMARY KEY,
    Nome nvarchar(30) NOT NULL,
    Preço money NOT NULL
);

CREATE TABLE Serviços (
    Id int PRIMARY KEY,
    Nome nvarchar(50) NOT NULL,
    Preço money NOT NULL
);

CREATE TABLE Avaliação (
    Id int PRIMARY KEY,
    Estrelas int NOT NULL,
    Texto nvarchar(250),
    fk_Serviços_Id int,
    fk_Pessoa_Id int
);

CREATE TABLE Administrador (
    Id_Administrativo int PRIMARY KEY,
    fk_Oficina_CNPJ nvarchar(14)
);

CREATE TABLE Pessoa (
	Id int PRIMARY KEY,
    Nome nvarchar(30) NOT NULL,
    Sobrenome nvarchar(30) NOT NULL,
    Estado nvarchar(25) NOT NULL,
    Cidade nvarchar(30) NOT NULL,
    Rua nvarchar(50) NOT NULL,
    Número int NOT NULL,
    Complemento nvarchar(30),
    fk_CelularTelefone_Id int,
    Email nvarchar(50) NOT NULL,
    CPF nvarchar(11) UNIQUE,
    CNPJ nvarchar(14) UNIQUE,
    Pessoa_TIPO INT NOT NULL,
);

CREATE TABLE CelularTelefone  (
    id int PRIMARY KEY,
    CelularTelefone nvarchar(11) NOT NULL
);

CREATE TABLE Oferece (
    fk_Oficina_CNPJ nvarchar(14),
    fk_Serviços_Id int,
    PRIMARY KEY (fk_Oficina_CNPJ, fk_Serviços_Id)
);

CREATE TABLE Contém (
    fk_Serviços_Id int,
    fk_Peça_Id int,
    PRIMARY KEY (fk_Serviços_Id, fk_Peça_Id)
);

CREATE TABLE Possui (
    fk_Serviços_Id int,
    fk_Orçamento_Id_Orçamento int,
    PRIMARY KEY (fk_Serviços_Id, fk_Orçamento_Id_Orçamento)
);
 
ALTER TABLE Orçamento ADD CONSTRAINT FK_Orçamento_2
    FOREIGN KEY (fk_Pessoa_Id)
    REFERENCES Pessoa (Id)
    ON DELETE CASCADE;
 
ALTER TABLE Orçamento ADD CONSTRAINT FK_Orçamento_3
    FOREIGN KEY (fk_Oficina_CNPJ)
    REFERENCES Oficina (CNPJ)
    ON DELETE CASCADE;
 
ALTER TABLE Orçamento ADD CONSTRAINT FK_Orçamento_4
    FOREIGN KEY (fk_Carro_Placa)
    REFERENCES Carro (Placa)
    ON DELETE NO ACTION;
 
ALTER TABLE Oficina ADD CONSTRAINT FK_Oficina_2
    FOREIGN KEY (fk_TelefoneCelular_Id)
    REFERENCES CelularTelefone  (Id)
    ON DELETE NO ACTION;
 
ALTER TABLE Carro ADD CONSTRAINT FK_Carro_2
    FOREIGN KEY (fk_Pessoa_Id)
    REFERENCES Pessoa (Id)
    ON DELETE CASCADE;
 
ALTER TABLE Avaliação ADD CONSTRAINT FK_Avaliação_2
    FOREIGN KEY (fk_Serviços_Id)
    REFERENCES Serviços (Id)
    ON DELETE NO ACTION;
 
ALTER TABLE Avaliação ADD CONSTRAINT FK_Avaliação_3
    FOREIGN KEY (fk_Pessoa_Id)
    REFERENCES Pessoa (Id)
    ON DELETE CASCADE;
 
ALTER TABLE Administrador ADD CONSTRAINT FK_Administrador_2
    FOREIGN KEY (fk_Oficina_CNPJ)
    REFERENCES Oficina (CNPJ)
    ON DELETE NO ACTION;
 
ALTER TABLE Pessoa ADD CONSTRAINT FK_Pessoa_2
    FOREIGN KEY (fk_CelularTelefone_Id)
    REFERENCES CelularTelefone  (Id)
    ON DELETE NO ACTION;
 
ALTER TABLE Oferece ADD CONSTRAINT FK_Oferece_1
    FOREIGN KEY (fk_Oficina_CNPJ)
    REFERENCES Oficina (CNPJ)
    ON DELETE NO ACTION;
 
ALTER TABLE Oferece ADD CONSTRAINT FK_Oferece_2
    FOREIGN KEY (fk_Serviços_Id)
    REFERENCES Serviços (Id)
    ON DELETE NO ACTION;
 
ALTER TABLE Contém ADD CONSTRAINT FK_Contém_1
    FOREIGN KEY (fk_Serviços_Id)
    REFERENCES Serviços (Id)
    ON DELETE NO ACTION;
 
ALTER TABLE Contém ADD CONSTRAINT FK_Contém_2
    FOREIGN KEY (fk_Peça_Id)
    REFERENCES Peça (Id)
    ON DELETE NO ACTION;
 
ALTER TABLE Possui ADD CONSTRAINT FK_Possui_1
    FOREIGN KEY (fk_Serviços_Id)
    REFERENCES Serviços (Id)
    ON DELETE NO ACTION;
 
ALTER TABLE Possui ADD CONSTRAINT FK_Possui_2
    FOREIGN KEY (fk_Orçamento_Id_Orçamento)
    REFERENCES Orçamento (Id_Orçamento)
    ON DELETE NO ACTION;
