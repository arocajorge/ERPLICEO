CREATE TABLE [dbo].[cp_conciliacion_Caja_det_Ing_Caja] (
    [IdEmpresa]           INT          NOT NULL,
    [IdConciliacion_Caja] NUMERIC (18) NOT NULL,
    [secuencia]           INT          NOT NULL,
    [IdEmpresa_movcaj]    INT          NOT NULL,
    [IdCbteCble_movcaj]   NUMERIC (18) NOT NULL,
    [IdTipocbte_movcaj]   INT          NOT NULL,
    [valor_aplicado]      FLOAT (53)   NOT NULL,
    [valor_disponible]    FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_cp_conciliacion_Caja_det_Ing_Caja] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdConciliacion_Caja] ASC, [secuencia] ASC),
    CONSTRAINT [FK_cp_conciliacion_Caja_det_Ing_Caja_caj_Caja_Movimiento] FOREIGN KEY ([IdEmpresa_movcaj], [IdCbteCble_movcaj], [IdTipocbte_movcaj]) REFERENCES [dbo].[caj_Caja_Movimiento] ([IdEmpresa], [IdCbteCble], [IdTipocbte]),
    CONSTRAINT [FK_cp_conciliacion_Caja_det_Ing_Caja_cp_conciliacion_Caja] FOREIGN KEY ([IdEmpresa], [IdConciliacion_Caja]) REFERENCES [dbo].[cp_conciliacion_Caja] ([IdEmpresa], [IdConciliacion_Caja])
);

