CREATE TABLE [dbo].[tb_sucursal] (
    [IdEmpresa]                INT           NOT NULL,
    [IdSucursal]               INT           NOT NULL,
    [codigo]                   VARCHAR (10)  NULL,
    [Su_Descripcion]           VARCHAR (100) NOT NULL,
    [Su_CodigoEstablecimiento] VARCHAR (30)  NULL,
    [Su_Ruc]                   VARCHAR (15)  NULL,
    [Su_JefeSucursal]          VARCHAR (100) NULL,
    [Su_Telefonos]             VARCHAR (50)  NULL,
    [Su_Direccion]             VARCHAR (100) NULL,
    [IdUsuario]                VARCHAR (20)  NULL,
    [Fecha_Transac]            DATETIME      NULL,
    [IdUsuarioUltMod]          VARCHAR (20)  NULL,
    [Fecha_UltMod]             DATETIME      NULL,
    [IdUsuarioUltAnu]          VARCHAR (20)  NULL,
    [Fecha_UltAnu]             DATETIME      NULL,
    [Estado]                   CHAR (1)      NOT NULL,
    [MotiAnula]                VARCHAR (200) NULL,
    [Es_establecimiento]       BIT           NOT NULL,
    [IdCtaCble_cxp]            VARCHAR (20)  NULL,
    [IdCtaCble_vtaIVA0]        VARCHAR (20)  NULL,
    [IdCtaCble_vtaIVA]         VARCHAR (20)  NULL,
    CONSTRAINT [PK_tb_sucursal] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC),
    CONSTRAINT [FK_tb_sucursal_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble_cxp]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble]),
    CONSTRAINT [FK_tb_sucursal_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);









