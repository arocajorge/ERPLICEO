CREATE TABLE [dbo].[ba_TipoFlujo] (
    [IdEmpresa]        INT           NOT NULL,
    [IdTipoFlujo]      NUMERIC (18)  NOT NULL,
    [IdTipoFlujoPadre] NUMERIC (18)  NULL,
    [Descricion]       VARCHAR (MAX) NOT NULL,
    [Estado]           CHAR (1)      NOT NULL,
    [IdUsuario]        VARCHAR (20)  NULL,
    [Fecha_Transac]    DATETIME      NULL,
    [IdUsuarioUltMod]  VARCHAR (20)  NULL,
    [Fecha_UltMod]     DATETIME      NULL,
    [IdUsuarioUltAnu]  VARCHAR (20)  NULL,
    [Fecha_UltAnu]     DATETIME      NULL,
    [MotiAnula]        VARCHAR (200) NULL,
    [Tipo]             VARCHAR (3)   NULL,
    [cod_flujo]        VARCHAR (50)  NULL,
    CONSTRAINT [PK_ba_TipoFlujo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTipoFlujo] ASC),
    CONSTRAINT [FK_ba_TipoFlujo_ba_TipoFlujo] FOREIGN KEY ([IdEmpresa], [IdTipoFlujoPadre]) REFERENCES [dbo].[ba_TipoFlujo] ([IdEmpresa], [IdTipoFlujo]),
    CONSTRAINT [FK_ba_TipoFlujo_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);



