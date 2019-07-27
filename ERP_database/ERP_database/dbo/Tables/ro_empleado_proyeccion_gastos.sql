CREATE TABLE [dbo].[ro_empleado_proyeccion_gastos] (
    [IdEmpresa]       INT            NOT NULL,
    [IdTransaccion]   NUMERIC (18)   NOT NULL,
    [IdEmpleado]      NUMERIC (18)   NOT NULL,
    [AnioFiscal]      INT            NOT NULL,
    [Observacion]     VARCHAR (1000) NULL,
    [estado]          BIT            NOT NULL,
    [IdUsuario]       VARCHAR (20)   NULL,
    [Fecha_Transac]   DATETIME       NULL,
    [IdUsuarioUltMod] VARCHAR (20)   NULL,
    [Fecha_UltMod]    DATETIME       NULL,
    [IdUsuarioUltAnu] VARCHAR (20)   NULL,
    [Fecha_UltAnu]    DATETIME       NULL,
    [MotiAnula]       VARCHAR (200)  NULL,
    CONSTRAINT [PK_ro_empleado_proyeccion_gastos] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdTransaccion] ASC),
    CONSTRAINT [FK_ro_empleado_proyeccion_gastos_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado])
);

