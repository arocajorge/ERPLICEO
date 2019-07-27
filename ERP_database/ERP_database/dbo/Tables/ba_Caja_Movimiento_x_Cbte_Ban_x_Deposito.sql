CREATE TABLE [dbo].[ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito] (
    [mcj_IdEmpresa]  INT          NOT NULL,
    [mcj_IdCbteCble] NUMERIC (18) NOT NULL,
    [mcj_IdTipocbte] INT          NOT NULL,
    [mba_IdEmpresa]  INT          NOT NULL,
    [mba_IdCbteCble] NUMERIC (18) NOT NULL,
    [mba_IdTipocbte] INT          NOT NULL,
    [mcj_Secuencia]  INT          NOT NULL,
    [Observacion]    VARCHAR (50) NULL,
    CONSTRAINT [PK_ba_Caja_Movimiento_x_Cbte_Ban_Deposito] PRIMARY KEY CLUSTERED ([mcj_IdEmpresa] ASC, [mcj_IdCbteCble] ASC, [mcj_IdTipocbte] ASC, [mba_IdEmpresa] ASC, [mba_IdCbteCble] ASC, [mba_IdTipocbte] ASC, [mcj_Secuencia] ASC),
    CONSTRAINT [FK_ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_ba_Cbte_Ban1] FOREIGN KEY ([mba_IdEmpresa], [mba_IdCbteCble], [mba_IdTipocbte]) REFERENCES [dbo].[ba_Cbte_Ban] ([IdEmpresa], [IdCbteCble], [IdTipocbte]),
    CONSTRAINT [FK_ba_Caja_Movimiento_x_Cbte_Ban_x_Deposito_caj_Caja_Movimiento] FOREIGN KEY ([mcj_IdEmpresa], [mcj_IdCbteCble], [mcj_IdTipocbte]) REFERENCES [dbo].[caj_Caja_Movimiento] ([IdEmpresa], [IdCbteCble], [IdTipocbte])
);

