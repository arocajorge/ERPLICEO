CREATE TABLE [dbo].[caj_Caja_Movimiento_det] (
    [IdEmpresa]    INT          NOT NULL,
    [IdCbteCble]   NUMERIC (18) NOT NULL,
    [IdTipocbte]   INT          NOT NULL,
    [Secuencia]    INT          NOT NULL,
    [IdCobro_tipo] VARCHAR (20) NULL,
    [cr_Valor]     FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_caj_Caja_Movimiento_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdCbteCble] ASC, [IdTipocbte] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_caj_Caja_Movimiento_det_caj_Caja_Movimiento] FOREIGN KEY ([IdEmpresa], [IdCbteCble], [IdTipocbte]) REFERENCES [dbo].[caj_Caja_Movimiento] ([IdEmpresa], [IdCbteCble], [IdTipocbte]),
    CONSTRAINT [FK_caj_Caja_Movimiento_det_cxc_cobro_tipo] FOREIGN KEY ([IdCobro_tipo]) REFERENCES [dbo].[cxc_cobro_tipo] ([IdCobro_tipo])
);

