CREATE TABLE [dbo].[ro_NominasPagosCheques_det] (
    [IdEmpresa]     INT           NOT NULL,
    [IdTransaccion] INT           NOT NULL,
    [Secuencia]     INT           NOT NULL,
    [IdSucursal]    INT           NOT NULL,
    [IdEmpleado]    NUMERIC (18)  NULL,
    [Observacion]   VARCHAR (MAX) NULL,
    [Valor]         FLOAT (53)    NOT NULL,
    [IdEmpresa_op]  INT           NOT NULL,
    [Secuancia_op]  INT           NOT NULL,
    [IdOrdenPago]   NUMERIC (18)  NOT NULL,
    [IdEmpresa_dc]  INT           NOT NULL,
    [IdTipoCbte]    INT           NOT NULL,
    [IdCbteCble]    NUMERIC (18)  NOT NULL,
    [Estado]        BIT           NOT NULL,
    CONSTRAINT [PK_ro_NominasPagosCheques_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTransaccion] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_NominasPagosCheques_det_cp_orden_pago_det] FOREIGN KEY ([IdEmpresa_op], [IdOrdenPago], [Secuancia_op]) REFERENCES [dbo].[cp_orden_pago_det] ([IdEmpresa], [IdOrdenPago], [Secuencia]),
    CONSTRAINT [FK_ro_NominasPagosCheques_det_ct_cbtecble] FOREIGN KEY ([IdEmpresa_dc], [IdTipoCbte], [IdCbteCble]) REFERENCES [dbo].[ct_cbtecble] ([IdEmpresa], [IdTipoCbte], [IdCbteCble]),
    CONSTRAINT [FK_ro_NominasPagosCheques_det_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_NominasPagosCheques_det_ro_NominasPagosCheques] FOREIGN KEY ([IdEmpresa], [IdTransaccion]) REFERENCES [dbo].[ro_NominasPagosCheques] ([IdEmpresa], [IdTransaccion]),
    CONSTRAINT [FK_ro_NominasPagosCheques_det_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);



