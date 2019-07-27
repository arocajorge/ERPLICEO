CREATE TABLE [dbo].[fa_factura_x_cxc_cobro] (
    [IdEmpresa]  INT          NOT NULL,
    [IdSucursal] INT          NOT NULL,
    [IdBodega]   INT          NOT NULL,
    [IdCbteVta]  NUMERIC (18) NOT NULL,
    [IdCobro]    NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_fa_factura_x_cxc_cobro] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdSucursal] ASC, [IdBodega] ASC, [IdCbteVta] ASC),
    CONSTRAINT [FK_fa_factura_x_cxc_cobro_cxc_cobro] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdCobro]) REFERENCES [dbo].[cxc_cobro] ([IdEmpresa], [IdSucursal], [IdCobro]),
    CONSTRAINT [FK_fa_factura_x_cxc_cobro_fa_factura] FOREIGN KEY ([IdEmpresa], [IdSucursal], [IdBodega], [IdCbteVta]) REFERENCES [dbo].[fa_factura] ([IdEmpresa], [IdSucursal], [IdBodega], [IdCbteVta])
);

