/* TCC_L�gico_5_ATUALIZADO (1): */

CREATE TABLE Or�amento (
    Valor money NOT NULL,
    Status int NOT NULL,
    Id_Or�amento int PRIMARY KEY,
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
    N�mero int NOT NULL,
    Complemento int
);

CREATE TABLE Carro (
    Placa nvarchar(7) PRIMARY KEY,
    Cor nvarchar(15) NOT NULL,
    Modelo nvarchar(25) NOT NULL,
    Motoriza��o decimal(2,1) NOT NULL,
    Marca nvarchar(15) NOT NULL,
    fk_Pessoa_Id int
);

CREATE TABLE Pe�a (
    Id int PRIMARY KEY,
    Nome nvarchar(30) NOT NULL,
    Pre�o int NOT NULL
);

CREATE TABLE Servi�os (
    Id int PRIMARY KEY,
    Nome nvarchar(50) NOT NULL,
    Pre�o money NOT NULL
);

CREATE TABLE Avalia��o (
    Id int PRIMARY KEY,
    Estrelas int NOT NULL,
    Texto nvarchar(250),
    fk_Servi�os_Id int,
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
    N�mero int NOT NULL,
    Complemento int,
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
    fk_Servi�os_Id int
);

CREATE TABLE Cont�m (
    fk_Servi�os_Id int,
    fk_Pe�a_Id int
);

CREATE TABLE Possui (
    fk_Servi�os_Id int,
    fk_Or�amento_Id_Or�amento int
);
 
ALTER TABLE Or�amento ADD CONSTRAINT FK_Or�amento_2
    FOREIGN KEY (fk_Pessoa_Id)
    REFERENCES Pessoa (Id)
    ON DELETE CASCADE;
 
ALTER TABLE Or�amento ADD CONSTRAINT FK_Or�amento_3
    FOREIGN KEY (fk_Oficina_CNPJ)
    REFERENCES Oficina (CNPJ)
    ON DELETE CASCADE;
 
ALTER TABLE Or�amento ADD CONSTRAINT FK_Or�amento_4
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
 
ALTER TABLE Avalia��o ADD CONSTRAINT FK_Avalia��o_2
    FOREIGN KEY (fk_Servi�os_Id)
    REFERENCES Servi�os (Id)
    ON DELETE NO ACTION;
 
ALTER TABLE Avalia��o ADD CONSTRAINT FK_Avalia��o_3
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
    FOREIGN KEY (fk_Servi�os_Id)
    REFERENCES Servi�os (Id)
    ON DELETE NO ACTION;
 
ALTER TABLE Cont�m ADD CONSTRAINT FK_Cont�m_1
    FOREIGN KEY (fk_Servi�os_Id)
    REFERENCES Servi�os (Id)
    ON DELETE NO ACTION;
 
ALTER TABLE Cont�m ADD CONSTRAINT FK_Cont�m_2
    FOREIGN KEY (fk_Pe�a_Id)
    REFERENCES Pe�a (Id)
    ON DELETE SET NULL;
 
ALTER TABLE Possui ADD CONSTRAINT FK_Possui_1
    FOREIGN KEY (fk_Servi�os_Id)
    REFERENCES Servi�os (Id)
    ON DELETE NO ACTION;
 
ALTER TABLE Possui ADD CONSTRAINT FK_Possui_2
    FOREIGN KEY (fk_Or�amento_Id_Or�amento)
    REFERENCES Or�amento (Id_Or�amento)
    ON DELETE SET NULL;