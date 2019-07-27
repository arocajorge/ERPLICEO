CREATE TABLE [dbo].[ct_anio_fiscal_x_tb_sucursal] (
    [IdEmpresa]    INT          NOT NULL,
    [IdanioFiscal] INT          NOT NULL,
    [IdSucursal]   INT          NOT NULL,
    [IdTipoCbte]   INT          NOT NULL,
    [IdCbteCble]   NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_ct_anio_fiscal_x_tb_sucursal] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdanioFiscal] ASC, [IdSucursal] ASC),
    CONSTRAINT [FK_ct_anio_fiscal_x_tb_sucursal_ct_anio_fiscal_x_cuenta_utilidad] FOREIGN KEY ([IdEmpresa], [IdanioFiscal]) REFERENCES [dbo].[ct_anio_fiscal_x_cuenta_utilidad] ([IdEmpresa], [IdanioFiscal]),
    CONSTRAINT [FK_ct_anio_fiscal_x_tb_sucursal_ct_cbtecble] FOREIGN KEY ([IdEmpresa], [IdTipoCbte], [IdCbteCble]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble]),
    CONSTRAINT [FK_ct_anio_fiscal_x_tb_sucursal_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);

