CREATE TABLE [dbo].[cp_conciliacion_Caja_ValesNoConciliados] (
    [IdEmpresa]           INT          NOT NULL,
    [IdConciliacion_Caja] NUMERIC (18) NOT NULL,
    [Secuencia]           INT          NOT NULL,
    [IdEmpresa_movcaja]   INT          NOT NULL,
    [IdCbteCble_movcaja]  NUMERIC (18) NOT NULL,
    [IdTipocbte_movcaja]  INT          NOT NULL,
    [Valor]               FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_cp_conciliacion_Caja_ValesNoConciliados] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdConciliacion_Caja] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cp_conciliacion_Caja_ValesNoConciliados_cp_conciliacion_Caja] FOREIGN KEY ([IdEmpresa], [IdConciliacion_Caja]) REFERENCES [dbo].[cp_conciliacion_Caja] ([IdEmpresa], [IdConciliacion_Caja])
);

