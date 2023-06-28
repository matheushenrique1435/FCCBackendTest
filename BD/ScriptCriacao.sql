CREATE DATABASE FCCBackendTest;

USE FCCBackendTest;

CREATE TABLE dbo.Clientes (
ID integer identity (1,1) not null,
CPF varchar(14) not null,
Nome varchar(200) not null,
RG varchar (12) not null,
DataExpedicao DateTime not null,
OrgaoExpedidor varchar(50) not null,
UF varchar(2),
DataNascimento DateTime,
Sexo varchar (1),
EstadoCivil varchar(20),
CONSTRAINT PK_Clientes PRIMARY KEY (ID) 
);

CREATE TABLE dbo.Enderecos (
ID integer identity (1,1) not null,
CEP varchar (9) not null,
Logradouro varchar (100) not null,
Numero varchar (6) not null,
Complemento varchar (20) not null,
Bairro varchar (30) not null,
Cidade varchar (30) not null,
UF varchar(2) not null,
clienteID integer not null,
CONSTRAINT PK_Enderecos PRIMARY KEY (ID),
CONSTRAINT FK_Clientes_Enderecos FOREIGN KEY (clienteID) REFERENCES Clientes (ID)
);