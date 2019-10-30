CREATE TABLE [dbo].[aca_Catalogo] (
    [IdCatalogo]            INT           NOT NULL,
    [IdCatalogoTipo]        INT           NOT NULL,
    [Codigo]                VARCHAR (50)  NULL,
    [NomCatalogo]           VARCHAR (500) NOT NULL,
    [Orden]                 INT           NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_Catalogo] PRIMARY KEY CLUSTERED ([IdCatalogo] ASC),
    CONSTRAINT [FK_aca_Catalogo_aca_CatalogoTipo] FOREIGN KEY ([IdCatalogoTipo]) REFERENCES [dbo].[aca_CatalogoTipo] ([IdCatalogoTipo])
);

