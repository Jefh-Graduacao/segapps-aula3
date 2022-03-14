CREATE DATABASE AtividadeAula3;

USE AtividadeAula3;

CREATE Table Produtos
(
	Id UNIQUEIDENTIFIER PRIMARY KEY,
	Nome VARCHAR(200),
	Preco DECIMAL(18, 2)
);

INSERT INTO Produtos VALUES
(NEWID(), 'Smartphone', 3500),
(NEWID(), 'Notebook', 5000),
(NEWID(), 'Televisão', 2400);