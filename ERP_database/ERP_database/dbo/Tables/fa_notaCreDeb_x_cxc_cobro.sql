CREATE TABLE [dbo].[fa_notaCreDeb_x_cxc_cobro] (
    [IdEmpresa_cbr]  INT          NOT NULL,
    [IdSucursal_cbr] INT          NOT NULL,
    [IdCobro_cbr]    NUMERIC (18) NOT NULL,
    [IdEmpresa_nt]   INT          NOT NULL,
    [IdSucursal_nt]  INT          NOT NULL,
    [IdBodega_nt]    INT          NOT NULL,
    [IdNota_nt]      NUMERIC (18) NOT NULL,
    [Valor_cobro]    FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_fa_notaCreDeb_x_cobro_x_factura_1] PRIMARY KEY CLUSTERED ([IdEmpresa_cbr] ASC, [IdSucursal_cbr] ASC, [IdCobro_cbr] ASC, [IdEmpresa_nt] ASC, [IdSucursal_nt] ASC, [IdBodega_nt] ASC, [IdNota_nt] ASC),
    CONSTRAINT [FK_fa_notaCreDeb_x_cxc_cobro_cxc_cobro] FOREIGN KEY ([IdEmpresa_cbr], [IdSucursal_cbr], [IdCobro_cbr]) REFERENCES [dbo].[cxc_cobro] ([IdEmpresa], [IdSucursal], [IdCobro]),
    CONSTRAINT [FK_fa_notaCreDeb_x_cxc_cobro_fa_notaCreDeb] FOREIGN KEY ([IdEmpresa_nt], [IdSucursal_nt], [IdBodega_nt], [IdNota_nt]) REFERENCES [dbo].[fa_notaCreDeb] ([IdEmpresa], [IdSucursal], [IdBodega], [IdNota])
);

