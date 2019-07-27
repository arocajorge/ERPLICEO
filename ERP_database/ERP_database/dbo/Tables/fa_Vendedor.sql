CREATE TABLE [dbo].[fa_Vendedor] (
    [IdEmpresa]       INT           NOT NULL,
    [IdVendedor]      INT           NOT NULL,
    [Codigo]          VARCHAR (50)  NULL,
    [Ve_Vendedor]     VARCHAR (500) NOT NULL,
    [NomInterno]      VARCHAR (500) NULL,
    [ve_cedula]       VARCHAR (20)  NULL,
    [PorComision]     FLOAT (53)    NOT NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [Estado]          VARCHAR (50)  NOT NULL,
    [MotivoAnula]     VARCHAR (200) NULL,
    CONSTRAINT [PK_fa_vendedor] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdVendedor] ASC),
    CONSTRAINT [FK_fa_Vendedor_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);



