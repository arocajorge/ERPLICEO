CREATE TABLE [dbo].[aca_CatalogoFicha] (
    [IdCatalogoFicha]       INT           NOT NULL,
    [IdCatalogoTipoFicha]   INT           NOT NULL,
    [Codigo]                VARCHAR (50)  NULL,
    [NomCatalogoFicha]      VARCHAR (500) NOT NULL,
    [Orden]                 INT           NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_CatalogoFicha] PRIMARY KEY CLUSTERED ([IdCatalogoFicha] ASC),
    CONSTRAINT [FK_aca_CatalogoFicha_aca_CatalogoTipoFicha] FOREIGN KEY ([IdCatalogoTipoFicha]) REFERENCES [dbo].[aca_CatalogoTipoFicha] ([IdCatalogoTipoFicha])
);

