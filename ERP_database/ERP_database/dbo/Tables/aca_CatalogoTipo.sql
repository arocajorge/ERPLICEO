CREATE TABLE [dbo].[aca_CatalogoTipo] (
    [IdCatalogoTipo]        INT           NOT NULL,
    [Codigo]                VARCHAR (50)  NOT NULL,
    [NomCatalogoTipo]       VARCHAR (500) NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_CatalogoTipo] PRIMARY KEY CLUSTERED ([IdCatalogoTipo] ASC)
);

