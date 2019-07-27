CREATE TABLE [dbo].[in_categorias] (
    [IdEmpresa]        INT           NOT NULL,
    [IdCategoria]      VARCHAR (25)  NOT NULL,
    [ca_Categoria]     VARCHAR (100) NOT NULL,
    [Estado]           CHAR (1)      NOT NULL,
    [IdCtaCtble_Inve]  VARCHAR (20)  NULL,
    [IdCtaCtble_Costo] VARCHAR (20)  NULL,
    [IdCtaCble_venta]  VARCHAR (20)  NULL,
    [IdUsuario]        VARCHAR (20)  NULL,
    [Fecha_Transac]    DATETIME      NULL,
    [nom_pc]           VARCHAR (50)  NULL,
    [ip]               VARCHAR (25)  NULL,
    [MotiAnula]        VARCHAR (200) NULL,
    [IdUsuarioUltMod]  VARCHAR (20)  NULL,
    [Fecha_UltMod]     DATETIME      NULL,
    [IdUsuarioUltAnu]  VARCHAR (20)  NULL,
    [Fecha_UltAnu]     DATETIME      NULL,
    [MotivoAnulacion]  VARCHAR (50)  NULL,
    [cod_categoria]    VARCHAR (50)  NULL,
    CONSTRAINT [PK_in_categorias] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCategoria] ASC),
    CONSTRAINT [FK_in_categorias_ct_plancta3] FOREIGN KEY ([IdEmpresa], [IdCtaCtble_Inve]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_in_categorias_ct_plancta4] FOREIGN KEY ([IdEmpresa], [IdCtaCtble_Costo]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_in_categorias_ct_plancta5] FOREIGN KEY ([IdEmpresa], [IdCtaCble_venta]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_in_categorias_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);



