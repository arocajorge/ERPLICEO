CREATE TABLE [dbo].[cp_orden_pago] (
    [IdEmpresa]           INT            NOT NULL,
    [IdOrdenPago]         NUMERIC (18)   NOT NULL,
    [IdSucursal]          INT            NOT NULL,
    [Observacion]         VARCHAR (MAX)  NULL,
    [IdTipo_op]           VARCHAR (20)   NOT NULL,
    [IdTipo_Persona]      VARCHAR (20)   NOT NULL,
    [IdPersona]           NUMERIC (18)   NOT NULL,
    [IdEntidad]           NUMERIC (18)   NOT NULL,
    [Fecha]               DATETIME       NOT NULL,
    [IdEstadoAprobacion]  VARCHAR (10)   NOT NULL,
    [IdFormaPago]         VARCHAR (20)   NOT NULL,
    [Estado]              VARCHAR (1)    NOT NULL,
    [ReferenciaGen]     VARCHAR(200)   NULL,
    [IdUsuarioAprobacion] VARCHAR (20)   NULL,
    [MotivoAprobacion]    VARCHAR (1000) NULL,
    [FechaAprobacion]     DATETIME       NULL,
    [IdUsuario]           VARCHAR (20)   NULL,
    [Fecha_Transac]       DATETIME       NULL,
    [IdUsuarioUltMod]     VARCHAR (20)   NULL,
    [FechaUltMod]         VARCHAR (20)   NULL,
    [IdUsuarioUltAnu]     VARCHAR (20)   NULL,
    [MotivoAnu]           VARCHAR (150)  NULL,
    [Fecha_UltAnu]        DATETIME       NULL,
    CONSTRAINT [PK_cp_orden_pago] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdOrdenPago] ASC),
    CONSTRAINT [FK_cp_orden_pago_cp_orden_pago_formapago] FOREIGN KEY ([IdFormaPago]) REFERENCES [dbo].[cp_orden_pago_formapago] ([IdFormaPago]),
    CONSTRAINT [FK_cp_orden_pago_cp_orden_pago_tipo] FOREIGN KEY ([IdTipo_op]) REFERENCES [dbo].[cp_orden_pago_tipo] ([IdTipo_op]),
    CONSTRAINT [FK_cp_orden_pago_tb_persona] FOREIGN KEY ([IdPersona]) REFERENCES [dbo].[tb_persona] ([IdPersona]),
    CONSTRAINT [FK_cp_orden_pago_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);





