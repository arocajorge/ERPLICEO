CREATE TABLE [dbo].[fa_proforma] (
    [IdEmpresa]              INT           NOT NULL,
    [IdSucursal]             INT           NOT NULL,
    [IdProforma]             NUMERIC (18)  NOT NULL,
    [IdCliente]              NUMERIC (18)  NOT NULL,
    [IdTerminoPago]          VARCHAR (20)  NOT NULL,
    [pf_plazo]               INT           NOT NULL,
    [pf_codigo]              VARCHAR (30)  NULL,
    [pf_observacion]         VARCHAR (MAX) NULL,
    [pf_fecha]               DATETIME      NOT NULL,
    [pf_fecha_vcto]          DATETIME      NOT NULL,
    [estado]                 BIT           NOT NULL,
    [IdUsuario_creacion]     VARCHAR (20)  NULL,
    [fecha_creacion]         DATETIME      NULL,
    [IdUsuario_modificacion] VARCHAR (20)  NULL,
    [fecha_modificacion]     DATETIME      NULL,
    [IdUsuario_anulacion]    VARCHAR (20)  NULL,
    [fecha_anulacion]        DATETIME      NULL,
    [MotivoAnulacion]        VARCHAR (500) NULL,
    [IdBodega]               INT           NOT NULL,
    [IdVendedor]             INT           NOT NULL,
    [pr_dias_entrega]        INT           NOT NULL,
    [IdNivel]                INT           NOT NULL,
    [IdCatalogo_FormaPago]   VARCHAR (15)  NULL,
    CONSTRAINT [PK_fa_proforma] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdProforma] ASC),
    CONSTRAINT [FK_fa_proforma_fa_cliente] FOREIGN KEY ([IdEmpresa], [IdCliente]) REFERENCES [dbo].[fa_cliente] ([IdEmpresa], [IdCliente]),
    CONSTRAINT [FK_fa_proforma_fa_NivelDescuento] FOREIGN KEY ([IdEmpresa], [IdNivel]) REFERENCES [dbo].[fa_NivelDescuento] ([IdEmpresa], [IdNivel]),
    CONSTRAINT [FK_fa_proforma_fa_TerminoPago] FOREIGN KEY ([IdTerminoPago]) REFERENCES [dbo].[fa_TerminoPago] ([IdTerminoPago]),
    CONSTRAINT [FK_fa_proforma_fa_Vendedor] FOREIGN KEY ([IdEmpresa], [IdVendedor]) REFERENCES [dbo].[fa_Vendedor] ([IdEmpresa], [IdVendedor]),
    CONSTRAINT [FK_fa_proforma_tb_bodega] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega]) REFERENCES [dbo].[tb_bodega] ([IdEmpresa], [IdSucursal], [IdBodega]),
    CONSTRAINT [FK_fa_proforma_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);







