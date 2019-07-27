CREATE TABLE [dbo].[ro_Parametros] (
    [IdEmpresa]                      INT          NOT NULL,
    [IdTipoCbte_AsientoSueldoXPagar] INT          NOT NULL,
    [Sueldo_basico]                  FLOAT (53)   NOT NULL,
    [Porcentaje_aporte_pers]         FLOAT (53)   NOT NULL,
    [Porcentaje_aporte_patr]         FLOAT (53)   NOT NULL,
    [IdRubro_acta_finiquito]         VARCHAR (50) NOT NULL,
    [genera_op_x_pago]               BIT          NOT NULL,
    [Genera_op_x_pago_x_empleao]     BIT          NOT NULL,
    [Genera_op_por_liq_vacaciones]   BIT          NOT NULL,
    [Genera_op_por_acta_finiquito]   BIT          NOT NULL,
    [Genera_op_por_prestamos]        BIT          NOT NULL,
    [IdTipo_op_vacaciones]           VARCHAR (20) NOT NULL,
    [IdTipo_op_prestamos]            VARCHAR (20) NOT NULL,
    [IdTipo_op_acta_finiquito]       VARCHAR (20) NOT NULL,
    [IdTipo_op_sueldo_por_pagar]     VARCHAR (20) NOT NULL,
    [EstadoCreacionPrestamos]        VARCHAR (10) NOT NULL,
    [Porcentaje_anticipo]            FLOAT (53)   NOT NULL,
    CONSTRAINT [PK_ro_Parametros] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC),
    CONSTRAINT [FK_ro_Parametros_cp_orden_pago_tipo_x_empresa] FOREIGN KEY ([IdEmpresa], [IdTipo_op_acta_finiquito]) REFERENCES [dbo].[cp_orden_pago_tipo_x_empresa] ([IdEmpresa], [IdTipo_op]),
    CONSTRAINT [FK_ro_Parametros_cp_orden_pago_tipo_x_empresa1] FOREIGN KEY ([IdEmpresa], [IdTipo_op_prestamos]) REFERENCES [dbo].[cp_orden_pago_tipo_x_empresa] ([IdEmpresa], [IdTipo_op]),
    CONSTRAINT [FK_ro_Parametros_cp_orden_pago_tipo_x_empresa2] FOREIGN KEY ([IdEmpresa], [IdTipo_op_sueldo_por_pagar]) REFERENCES [dbo].[cp_orden_pago_tipo_x_empresa] ([IdEmpresa], [IdTipo_op]),
    CONSTRAINT [FK_ro_Parametros_ro_catalogo] FOREIGN KEY ([EstadoCreacionPrestamos]) REFERENCES [dbo].[ro_catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_ro_Parametros_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);







