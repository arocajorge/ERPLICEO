CREATE TABLE [dbo].[aca_CatalogoTipoFicha] (
    [IdCatalogoTipoFicha]   INT           NOT NULL,
    [Codigo]                VARCHAR (50)  NOT NULL,
    [NomCatalogoTipoFicha]  VARCHAR (500) NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_CatalogoTipoFicha] PRIMARY KEY CLUSTERED ([IdCatalogoTipoFicha] ASC)
);

