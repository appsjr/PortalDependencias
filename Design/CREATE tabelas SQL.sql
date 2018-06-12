CREATE TABLE [dbo].[DTOCategorias] (
    [Codigo]    INT           IDENTITY (1, 1) NOT NULL,
    [Descricao] VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Codigo] ASC)
);
GO
CREATE TABLE [dbo].[DTOPendencias] (
    [ID]          UNIQUEIDENTIFIER NOT NULL,
    [Titulo]      NVARCHAR (200)   NULL,
    [Descricao]   NVARCHAR (1000)  NULL,
    [Status]      INT              NOT NULL,
    [Responsavel] NVARCHAR (20)    NULL,
    [Obs]         NVARCHAR (1000)  NULL,
    [Data]        DATETIME         NULL,
    [CategoriaID] INT              NULL,
    CONSTRAINT [PK_dbo.DTOPendencias] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO
CREATE TABLE [dbo].[login] (
    [ID]        INT          IDENTITY (1, 1) NOT NULL,
    [Username]  VARCHAR (50) NULL,
    [Password]  VARCHAR (10) NULL,
    [Descricao] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


