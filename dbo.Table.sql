IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Products' and xtype='U')
CREATE TABLE [dbo].[Products] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL,
    [Article]  NVARCHAR(50)  NOT NULL,
    [Name]     NVARCHAR(250)  NOT NULL,
    [Price]    NUMERIC(15,2) NOT NULL DEFAULT 0,
    [Quantity] INT             NOT NULL DEFAULT 0,
    CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED ([Id] ASC)
);