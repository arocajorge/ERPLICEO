CREATE TABLE [dbo].[in_movi_inven_tipo] (
    [IdEmpresa]              INT           NOT NULL,
    [IdMovi_inven_tipo]      INT           NOT NULL,
    [Codigo]                 VARCHAR (50)  NULL,
    [tm_descripcion]         VARCHAR (200) NOT NULL,
    [cm_tipo_movi]           CHAR (1)      NOT NULL,
    [cm_descripcionCorta]    VARCHAR (10)  NOT NULL,
    [Estado]                 CHAR (1)      NOT NULL,
    [IdTipoCbte]             INT           NULL,
    [IdUsuario]              VARCHAR (50)  NULL,
    [Fecha_Transac]          DATETIME      NULL,
    [IdUsuarioUltMod]        VARCHAR (50)  NULL,
    [Fecha_UltMod]           DATETIME      NULL,
    [IdUsuarioUltAnu]        VARCHAR (50)  NULL,
    [Fecha_UltAnu]           DATETIME      NULL,
    [MotiAnula]              VARCHAR (MAX) NULL,
    [Genera_Diario_Contable] BIT           NOT NULL,
    [IdCatalogoAprobacion]   VARCHAR (15)  NOT NULL,
    CONSTRAINT [PK_in_movi_inven_tipo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMovi_inven_tipo] ASC),
    CONSTRAINT [FK_in_movi_inven_tipo_in_Catalogo] FOREIGN KEY ([IdCatalogoAprobacion]) REFERENCES [dbo].[in_Catalogo] ([IdCatalogo])
);





