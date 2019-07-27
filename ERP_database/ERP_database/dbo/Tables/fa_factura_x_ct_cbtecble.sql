CREATE TABLE [dbo].[fa_factura_x_ct_cbtecble] (
    [vt_IdEmpresa]  INT          NOT NULL,
    [vt_IdSucursal] INT          NOT NULL,
    [vt_IdBodega]   INT          NOT NULL,
    [vt_IdCbteVta]  NUMERIC (18) NOT NULL,
    [ct_IdEmpresa]  INT          NOT NULL,
    [ct_IdTipoCbte] INT          NOT NULL,
    [ct_IdCbteCble] NUMERIC (18) NOT NULL,
    [Motivo]        VARCHAR (50) NULL,
    CONSTRAINT [PK_fa_factura_x_ct_cbtecble] PRIMARY KEY CLUSTERED ([vt_IdEmpresa] ASC, [vt_IdSucursal] ASC, [vt_IdBodega] ASC, [vt_IdCbteVta] ASC, [ct_IdEmpresa] ASC, [ct_IdTipoCbte] ASC, [ct_IdCbteCble] ASC),
    CONSTRAINT [FK_fa_factura_x_ct_cbtecble_ct_cbtecble] FOREIGN KEY ([ct_IdEmpresa], [ct_IdTipoCbte], [ct_IdCbteCble]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble]),
    CONSTRAINT [FK_fa_factura_x_ct_cbtecble_fa_factura] FOREIGN KEY ([vt_IdEmpresa], [vt_IdSucursal], [vt_IdBodega], [vt_IdCbteVta]) REFERENCES [dbo].[fa_factura] ([IdEmpresa], [IdSucursal], [IdBodega], [IdCbteVta])
);

