CREATE TABLE [dbo].[fa_factura] (
    [IdEmpresa]            INT           NOT NULL,
    [IdSucursal]           INT           NOT NULL,
    [IdBodega]             INT           NOT NULL,
    [IdCbteVta]            NUMERIC (18)  NOT NULL,
    [CodCbteVta]           VARCHAR (20)  NULL,
    [vt_tipoDoc]           VARCHAR (20)  NOT NULL,
    [vt_serie1]            VARCHAR (3)   NULL,
    [vt_serie2]            VARCHAR (3)   NULL,
    [vt_NumFactura]        VARCHAR (20)  NULL,
    [Fecha_Autorizacion]   DATETIME      NULL,
    [vt_autorizacion]      VARCHAR (50)  NULL,
    [IdCliente]            NUMERIC (18)  NOT NULL,
    [IdVendedor]           INT           NOT NULL,
    [IdNivel]              INT           NOT NULL,
    [IdCatalogo_FormaPago] VARCHAR (15)  NULL,
    [vt_fecha]             DATETIME      NOT NULL,
    [vt_plazo]             NUMERIC (10)  NOT NULL,
    [vt_fech_venc]         DATETIME      NOT NULL,
    [vt_tipo_venta]        VARCHAR (20)  NOT NULL,
    [vt_Observacion]       VARCHAR (500) NOT NULL,
    [Estado]               CHAR (1)      NOT NULL,
    [IdCaja]               INT           NOT NULL,
    [IdUsuario]            VARCHAR (20)  NULL,
    [Fecha_Transaccion]    DATETIME      NULL,
    [IdUsuarioUltModi]     VARCHAR (20)  NULL,
    [Fecha_UltMod]         DATETIME      NULL,
    [IdUsuarioUltAnu]      VARCHAR (20)  NULL,
    [Fecha_UltAnu]         DATETIME      NULL,
    [MotivoAnulacion]      VARCHAR (100) NULL,
    [IdPuntoVta]           INT           NULL,
    [esta_impresa]         BIT           NULL,
    [fecha_primera_cuota]  DATETIME      NULL,
    [valor_abono]          FLOAT (53)    NULL,
    [aprobada_enviar_sri]  BIT           NOT NULL,
    [Generado]             BIT           NULL,
    CONSTRAINT [PK_fa_factura] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdCbteVta] ASC),
    CONSTRAINT [FK_fa_factura_caj_Caja] FOREIGN KEY ([IdEmpresa], [IdCaja]) REFERENCES [dbo].[caj_Caja] ([IdEmpresa], [IdCaja]),
    CONSTRAINT [FK_fa_factura_fa_catalogo] FOREIGN KEY ([IdCatalogo_FormaPago]) REFERENCES [dbo].[fa_catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_fa_factura_fa_cliente] FOREIGN KEY ([IdEmpresa], [IdCliente]) REFERENCES [dbo].[fa_cliente] ([IdEmpresa], [IdCliente]),
    CONSTRAINT [FK_fa_factura_fa_NivelDescuento] FOREIGN KEY ([IdEmpresa], [IdNivel]) REFERENCES [dbo].[fa_NivelDescuento] ([IdEmpresa], [IdNivel]),
    CONSTRAINT [FK_fa_factura_fa_PuntoVta] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdPuntoVta]) REFERENCES [dbo].[fa_PuntoVta] ([IdEmpresa], [IdSucursal], [IdPuntoVta]),
    CONSTRAINT [FK_fa_factura_fa_Vendedor] FOREIGN KEY ([IdEmpresa], [IdVendedor]) REFERENCES [dbo].[fa_Vendedor] ([IdEmpresa], [IdVendedor]),
    CONSTRAINT [FK_fa_factura_tb_bodega] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega]) REFERENCES [dbo].[tb_bodega] ([IdEmpresa], [IdSucursal], [IdBodega]),
    CONSTRAINT [FK_fa_factura_tb_sis_Documento_Tipo_Talonario] FOREIGN KEY ([IdEmpresa], [vt_tipoDoc], [vt_serie2], [vt_serie1], [vt_NumFactura]) REFERENCES [dbo].[tb_sis_Documento_Tipo_Talonario] ([IdEmpresa], [CodDocumentoTipo], [PuntoEmision], [Establecimiento], [NumDocumento])
);
















GO
CREATE NONCLUSTERED INDEX [IX_fa_factura]
    ON [dbo].[fa_factura]([IdEmpresa] ASC, [vt_tipoDoc] ASC, [vt_serie1] ASC, [vt_serie2] ASC, [vt_NumFactura] ASC);

