CREATE TABLE [dbo].[cp_conciliacion_Caja] (
    [IdEmpresa]             INT            NOT NULL,
    [IdConciliacion_Caja]   NUMERIC (18)   NOT NULL,
    [IdPeriodo]             INT            NOT NULL,
    [Fecha_ini]             DATE           NOT NULL,
    [Fecha_fin]             DATE           NOT NULL,
    [Fecha]                 DATE           NOT NULL,
    [IdCaja]                INT            NOT NULL,
    [IdEstadoCierre]        VARCHAR (25)   NOT NULL,
    [Observacion]           VARCHAR (1000) NULL,
    [IdEmpresa_op]          INT            NULL,
    [IdOrdenPago_op]        NUMERIC (18)   NULL,
    [IdCtaCble]             VARCHAR (20)   NOT NULL,
    [Saldo_cont_al_periodo] FLOAT (53)     NOT NULL,
    [Ingresos]              FLOAT (53)     NOT NULL,
    [Total_Ing]             FLOAT (53)     NOT NULL,
    [Total_fact_vale]       FLOAT (53)     NOT NULL,
    [Total_fondo]           FLOAT (53)     NOT NULL,
    [Dif_x_pagar_o_cobrar]  FLOAT (53)     NOT NULL,
    [IdTipoFlujo]           NUMERIC (18)   NULL,
    [IdEmpresa_mov_caj]     INT            NULL,
    [IdTipoCbte_mov_caj]    INT            NULL,
    [IdCbteCble_mov_caj]    NUMERIC (18)   NULL,
    [SecuenciaCaja]         NUMERIC (18)   NULL,
    [IdUsuarioCreacion]     VARCHAR (50)   NULL,
    [FechaCreacion]         DATETIME       NULL,
    [IdUsuarioModificacion] VARCHAR (50)   NULL,
    [FechaModificacion]     DATETIME       NULL,
    CONSTRAINT [PK_cp_conciliacion_Caja_] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdConciliacion_Caja] ASC),
    CONSTRAINT [FK_cp_conciliacion_Caja_ba_TipoFlujo] FOREIGN KEY ([IdEmpresa], [IdTipoFlujo]) REFERENCES [dbo].[ba_TipoFlujo] ([IdEmpresa], [IdTipoFlujo]),
    CONSTRAINT [FK_cp_conciliacion_Caja_caj_Caja] FOREIGN KEY ([IdEmpresa], [IdCaja]) REFERENCES [dbo].[caj_Caja] ([IdEmpresa], [IdCaja]),
    CONSTRAINT [FK_cp_conciliacion_Caja_caj_Caja_Movimiento] FOREIGN KEY ([IdEmpresa_mov_caj], [IdCbteCble_mov_caj], [IdTipoCbte_mov_caj]) REFERENCES [dbo].[caj_Caja_Movimiento] ([IdEmpresa], [IdCbteCble], [IdTipocbte]),
    CONSTRAINT [FK_cp_conciliacion_Caja_cp_catalogo] FOREIGN KEY ([IdEstadoCierre]) REFERENCES [dbo].[cp_catalogo] ([IdCatalogo]),
    CONSTRAINT [FK_cp_conciliacion_Caja_cp_orden_pago] FOREIGN KEY ([IdEmpresa_op], [IdOrdenPago_op]) REFERENCES [dbo].[cp_orden_pago] ([IdEmpresa], [IdOrdenPago]),
    CONSTRAINT [FK_cp_conciliacion_Caja_ct_periodo] FOREIGN KEY ([IdEmpresa], [IdPeriodo]) REFERENCES [dbo].[ct_periodo] ([IdEmpresa], [IdPeriodo]),
    CONSTRAINT [FK_cp_conciliacion_Caja_ct_plancta] FOREIGN KEY ([IdEmpresa], [IdCtaCble]) REFERENCES [dbo].[ct_plancta] ([IdEmpresa], [IdCtaCble])
);





