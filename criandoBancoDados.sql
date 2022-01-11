CREATE DATABASE Bitnvest;

USE Bitnvest;


IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Administradores] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [Senha] nvarchar(max) NULL,
    CONSTRAINT [PK_Administradores] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Contas] (
    [Id] int NOT NULL IDENTITY,
    [Numero] int NOT NULL,
    [Saldo] decimal(18, 2) NOT NULL,
    CONSTRAINT [PK_Contas] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Moedas] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [Quantidade] int NOT NULL,
    [Cotacao] decimal(18, 2) NOT NULL,
    [DataCotacao] datetime2 NOT NULL,
    [DataAtualizacao] datetime2 NULL,
    CONSTRAINT [PK_Moedas] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Tarifas] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [Valor] decimal(18, 2) NOT NULL,
    [PagamentoEmDias] int NOT NULL,
    CONSTRAINT [PK_Tarifas] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Correntistas] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [Senha] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [CPF] nvarchar(max) NULL,
    [DataNascimento] datetime2 NOT NULL,
    [Cel] nvarchar(max) NULL,
    [IdConta] int NOT NULL,
    CONSTRAINT [PK_Correntistas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Correntistas_Contas_IdConta] FOREIGN KEY ([IdConta]) REFERENCES [Contas] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Transacoes] (
    [Id] int NOT NULL IDENTITY,
    [ContaId] int NULL,
    [Quantidade] int NOT NULL,
    [Valor] decimal(18, 2) NOT NULL,
    [TipoTransacao] int NOT NULL,
    [DataTransacao] datetime2 NOT NULL,
    CONSTRAINT [PK_Transacoes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transacoes_Contas_ContaId] FOREIGN KEY ([ContaId]) REFERENCES [Contas] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [PagamentosTarifas] (
    [Id] int NOT NULL IDENTITY,
    [IdCorrentista] int NOT NULL,
    [IdTarifa] int NOT NULL,
    [DataVencimento] datetime2 NOT NULL,
    [DataPagamento] datetime2 NULL,
    CONSTRAINT [PK_PagamentosTarifas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PagamentosTarifas_Correntistas_IdCorrentista] FOREIGN KEY ([IdCorrentista]) REFERENCES [Correntistas] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_PagamentosTarifas_Tarifas_IdTarifa] FOREIGN KEY ([IdTarifa]) REFERENCES [Tarifas] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [MoedaTransacoes] (
    [Id] int NOT NULL IDENTITY,
    [Nome] nvarchar(max) NULL,
    [Quantidade] int NOT NULL,
    [Cotacao] decimal(18, 2) NOT NULL,
    [ValorTotal] decimal(18, 2) NOT NULL,
    [TransacaoId] int NULL,
    CONSTRAINT [PK_MoedaTransacoes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MoedaTransacoes_Transacoes_TransacaoId] FOREIGN KEY ([TransacaoId]) REFERENCES [Transacoes] ([Id]) ON DELETE NO ACTION
);

GO

CREATE UNIQUE INDEX [IX_Correntistas_IdConta] ON [Correntistas] ([IdConta]);

GO

CREATE INDEX [IX_MoedaTransacoes_TransacaoId] ON [MoedaTransacoes] ([TransacaoId]);

GO

CREATE INDEX [IX_PagamentosTarifas_IdCorrentista] ON [PagamentosTarifas] ([IdCorrentista]);

GO

CREATE INDEX [IX_PagamentosTarifas_IdTarifa] ON [PagamentosTarifas] ([IdTarifa]);

GO

CREATE INDEX [IX_Transacoes_ContaId] ON [Transacoes] ([ContaId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201123055528_CreateDatabase', N'2.1.14-servicing-32113');

GO



INSERT INTO Bitnvest.dbo.Administradores (Nome,Email,Senha) VALUES 
('Administrador','admin@admin.com','123')
,('ERICK','erick@erick.com','123')
;INSERT INTO Bitnvest.dbo.Contas (Numero,Saldo) VALUES 
(6256,3477.58)
,(6083,3488.79)
;INSERT INTO Bitnvest.dbo.Correntistas (Nome,Senha,Email,CPF,DataNascimento,Cel,IdConta) VALUES 
('ADMINISTRADOR','123','admin@admin.com','123.456.789-09','2020-11-01 00:00:00.000','(11) 12312-3123',1)
,('FERNANDO','123','fnd_broz@hotmail.com','123.456.789-09','2020-12-01 00:00:00.000','(11) 12312-3123',2)
;INSERT INTO Bitnvest.dbo.MoedaTransacoes (Nome,Quantidade,Cotacao,ValorTotal,TransacaoId) VALUES 
('Moeda 3',0,13.77,0.00,1)
,('Moeda 2',0,8.27,0.00,1)
,('Moeda 1',0,7.75,0.00,1)
,('Moeda 3',0,13.77,0.00,2)
,('Moeda 2',0,8.27,0.00,2)
,('Moeda 1',0,7.75,0.00,2)
,('Moeda 3',0,13.77,0.00,3)
,('Moeda 2',0,8.27,0.00,3)
,('Moeda 1',0,7.75,0.00,3)
,('Moeda 3',0,13.77,0.00,4)
;
INSERT INTO Bitnvest.dbo.MoedaTransacoes (Nome,Quantidade,Cotacao,ValorTotal,TransacaoId) VALUES 
('Moeda 2',0,8.27,0.00,4)
,('Moeda 1',0,7.75,0.00,4)
,('Moeda 3',0,13.77,0.00,5)
,('Moeda 2',0,8.27,0.00,5)
,('Moeda 1',0,7.75,0.00,5)
,('Moeda 3',0,13.40,0.00,6)
,('Moeda 2',0,1.45,0.00,6)
,('Moeda 1',0,6.60,0.00,6)
,('Moeda 3',0,13.40,0.00,7)
,('Moeda 2',0,1.45,0.00,7)
;
INSERT INTO Bitnvest.dbo.MoedaTransacoes (Nome,Quantidade,Cotacao,ValorTotal,TransacaoId) VALUES 
('Moeda 1',0,6.60,0.00,7)
;INSERT INTO Bitnvest.dbo.Moedas (Nome,Quantidade,Cotacao,DataCotacao,DataAtualizacao) VALUES 
('Moeda 1',10000,7.75,'2020-11-23 02:56:18.335','2020-11-23 02:56:18.335')
,('Moeda 2',10000,8.27,'2020-11-23 02:56:18.339','2020-11-23 02:56:18.335')
,('Moeda 3',10000,13.77,'2020-11-23 02:56:18.340','2020-11-23 02:56:18.335')
,('Moeda 1',10000,6.60,'2020-12-08 03:21:45.729','2020-12-08 03:21:45.728')
,('Moeda 2',10000,1.45,'2020-12-08 03:21:45.731','2020-12-08 03:21:45.728')
,('Moeda 3',10000,13.40,'2020-12-08 03:21:45.731','2020-12-08 03:21:45.728')
;INSERT INTO Bitnvest.dbo.PagamentosTarifas (IdCorrentista,IdTarifa,DataVencimento,DataPagamento) VALUES 
(1,1,'2020-12-08 00:00:00.000','2020-12-08 00:00:00.000')
,(1,1,'2020-12-23 00:00:00.000',NULL)
,(1,1,'2021-01-07 00:00:00.000','2021-01-07 00:00:00.000')
,(1,2,'2020-12-08 00:00:00.000','2020-12-08 00:00:00.000')
,(1,2,'2020-12-23 00:00:00.000',NULL)
,(1,2,'2021-01-07 00:00:00.000','2021-01-07 00:00:00.000')
,(1,1,'2021-01-22 00:00:00.000',NULL)
,(1,2,'2021-01-22 00:00:00.000',NULL)
,(2,1,'2020-12-23 00:00:00.000',NULL)
,(2,1,'2021-01-07 00:00:00.000','2021-01-07 00:00:00.000')
;
INSERT INTO Bitnvest.dbo.PagamentosTarifas (IdCorrentista,IdTarifa,DataVencimento,DataPagamento) VALUES 
(2,1,'2021-01-22 00:00:00.000',NULL)
,(2,2,'2020-12-23 00:00:00.000',NULL)
,(2,2,'2021-01-07 00:00:00.000','2021-01-07 00:00:00.000')
,(2,2,'2021-01-22 00:00:00.000',NULL)
,(1,1,'2021-02-06 00:00:00.000',NULL)
,(1,2,'2021-02-06 00:00:00.000',NULL)
,(2,1,'2021-02-06 00:00:00.000',NULL)
,(2,2,'2021-02-06 00:00:00.000',NULL)
;INSERT INTO Bitnvest.dbo.Tarifas (Nome,Valor,PagamentoEmDias) VALUES 
('Cesta',10.10,15)
,('Teste',1.11,15)
;INSERT INTO Bitnvest.dbo.Transacoes (ContaId,Quantidade,Valor,TipoTransacao,DataTransacao) VALUES 
(1,0,29.79,2,'2020-11-23 02:56:25.300')
,(1,0,29.79,1,'2020-11-23 02:56:29.887')
,(1,0,7.75,2,'2020-11-23 02:56:32.968')
,(1,0,7.75,2,'2020-11-23 02:56:35.667')
,(1,0,15.50,1,'2020-11-23 02:56:39.396')
,(2,0,21.45,2,'2020-12-08 03:21:53.995')
,(2,0,21.45,1,'2020-12-08 03:22:02.086')
;INSERT INTO Bitnvest.dbo.[__EFMigrationsHistory] (MigrationId,ProductVersion) VALUES 
('20201123055528_CreateDatabase','2.1.14-servicing-32113')
;