CREATE TABLE [dbo].[cxc_cobro_x_caj_Caja_Movimiento] (
    [cbr_IdEmpresa]  INT          NOT NULL,
    [cbr_IdSucursal] INT          NOT NULL,
    [cbr_IdCobro]    NUMERIC (18) NOT NULL,
    [mcj_IdEmpresa]  INT          NOT NULL,
    [mcj_IdCbteCble] NUMERIC (18) NOT NULL,
    [mcj_IdTipocbte] INT          NOT NULL,
    [observacion]    VARCHAR (50) NULL,
    CONSTRAINT [PK_cxc_cobro_x_caj_Caja_Movimiento] PRIMARY KEY CLUSTERED ([cbr_IdEmpresa] ASC, [cbr_IdSucursal] ASC, [cbr_IdCobro] ASC, [mcj_IdEmpresa] ASC, [mcj_IdCbteCble] ASC, [mcj_IdTipocbte] ASC),
    CONSTRAINT [FK_cxc_cobro_x_caj_Caja_Movimiento_caj_Caja_Movimiento] FOREIGN KEY ([mcj_IdEmpresa], [mcj_IdCbteCble], [mcj_IdTipocbte]) REFERENCES [dbo].[caj_Caja_Movimiento] ([IdEmpresa], [IdCbteCble], [IdTipocbte]),
    CONSTRAINT [FK_cxc_cobro_x_caj_Caja_Movimiento_cxc_cobro] FOREIGN KEY ([cbr_IdEmpresa], [cbr_IdSucursal], [cbr_IdCobro]) REFERENCES [dbo].[cxc_cobro] ([IdEmpresa], [IdSucursal], [IdCobro])
);

