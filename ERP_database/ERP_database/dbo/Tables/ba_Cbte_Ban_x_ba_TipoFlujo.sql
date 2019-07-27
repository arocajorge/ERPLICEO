CREATE TABLE [dbo].[ba_Cbte_Ban_x_ba_TipoFlujo] (
    [IdEmpresa]   INT          NOT NULL,
    [IdTipocbte]  INT          NOT NULL,
    [IdCbteCble]  NUMERIC (18) NOT NULL,
    [Secuencia]   INT          NOT NULL,
    [IdTipoFlujo] NUMERIC (18) NOT NULL,
    [Porcentaje]  FLOAT (53)   NOT NULL,
    [Valor]       FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_ba_Cbte_Ban_x_ba_TipoFlujo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTipocbte] ASC, [IdCbteCble] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ba_Cbte_Ban_x_ba_TipoFlujo_ba_Cbte_Ban] FOREIGN KEY ([IdEmpresa], [IdCbteCble], [IdTipocbte]) REFERENCES [dbo].[ba_Cbte_Ban] ([IdEmpresa], [IdCbteCble], [IdTipocbte]),
    CONSTRAINT [FK_ba_Cbte_Ban_x_ba_TipoFlujo_ba_TipoFlujo] FOREIGN KEY ([IdEmpresa], [IdTipoFlujo]) REFERENCES [dbo].[ba_TipoFlujo] ([IdEmpresa], [IdTipoFlujo])
);

