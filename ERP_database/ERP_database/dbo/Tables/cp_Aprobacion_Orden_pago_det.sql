CREATE TABLE [dbo].[cp_Aprobacion_Orden_pago_det] (
    [IdEmpresa]      INT          NOT NULL,
    [IdAprobacion]   NUMERIC (18) NOT NULL,
    [secuencia]      INT          NOT NULL,
    [IdEmpresa_OP]   INT          NOT NULL,
    [IdOrdenPago_OP] NUMERIC (18) NOT NULL,
    [Secuencia_OP]   INT          NOT NULL,
    CONSTRAINT [PK_cp_Aprobacion_Orden_pago_det] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAprobacion] ASC, [secuencia] ASC),
    CONSTRAINT [FK_cp_Aprobacion_Orden_pago_det_cp_Aprobacion_Orden_pago] FOREIGN KEY ([IdEmpresa], [IdAprobacion]) REFERENCES [dbo].[cp_Aprobacion_Orden_pago] ([IdEmpresa], [IdAprobacion]),
    CONSTRAINT [FK_cp_Aprobacion_Orden_pago_det_cp_orden_pago_det] FOREIGN KEY ([IdEmpresa_OP], [IdOrdenPago_OP], [Secuencia_OP]) REFERENCES [dbo].[cp_orden_pago_det] ([IdEmpresa], [IdOrdenPago], [Secuencia])
);

