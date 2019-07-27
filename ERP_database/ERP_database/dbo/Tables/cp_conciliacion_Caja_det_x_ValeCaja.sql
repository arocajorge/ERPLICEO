CREATE TABLE [dbo].[cp_conciliacion_Caja_det_x_ValeCaja] (
    [IdEmpresa]                      INT          NOT NULL,
    [IdConciliacion_Caja]            NUMERIC (18) NOT NULL,
    [Secuencia]                      INT          NOT NULL,
    [IdEmpresa_movcaja]              INT          NOT NULL,
    [IdCbteCble_movcaja]             NUMERIC (18) NOT NULL,
    [IdTipocbte_movcaja]             INT          NOT NULL,
    [IdCtaCble]                      VARCHAR (20) NOT NULL,
    [IdPunto_cargo]                  INT          NULL,
    [IdPunto_cargo_grupo]            INT          NULL,
    [IdCentroCosto]                  VARCHAR (20) NULL,
    [IdCentroCosto_sub_centro_costo] VARCHAR (20) NULL,
    CONSTRAINT [PK_cp_conciliacion_Caja_det_x_ValeCaja] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdConciliacion_Caja] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_cp_conciliacion_Caja_det_x_ValeCaja_caj_Caja_Movimiento] FOREIGN KEY ([IdEmpresa_movcaja], [IdCbteCble_movcaja], [IdTipocbte_movcaja]) REFERENCES [dbo].[caj_Caja_Movimiento] ([IdEmpresa], [IdCbteCble], [IdTipocbte]),
    CONSTRAINT [FK_cp_conciliacion_Caja_det_x_ValeCaja_cp_conciliacion_Caja] FOREIGN KEY ([IdEmpresa], [IdConciliacion_Caja]) REFERENCES [dbo].[cp_conciliacion_Caja] ([IdEmpresa], [IdConciliacion_Caja])
);

