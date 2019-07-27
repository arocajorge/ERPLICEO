CREATE TABLE [dbo].[ro_marcaciones_x_empleado] (
    [IdEmpresa]         INT           NOT NULL,
    [IdRegistro]        NUMERIC (18)  NOT NULL,
    [IdCalendadrio]     INT           NOT NULL,
    [IdEmpleado]        NUMERIC (18)  NOT NULL,
    [IdTipoMarcaciones] VARCHAR (10)  NOT NULL,
    [IdNomina]          INT           NULL,
    [IdSucursal]        INT           NULL,
    [es_Hora]           TIME (0)      NULL,
    [es_fechaRegistro]  DATETIME      NOT NULL,
    [Observacion]       VARCHAR (200) NULL,
    [IdUsuario]         VARCHAR (20)  NOT NULL,
    [Estado]            CHAR (1)      NOT NULL,
    [Fecha_Transac]     DATETIME      NOT NULL,
    [Ip]                VARCHAR (30)  NULL,
    [IdUsuarioUltModi]  VARCHAR (20)  NULL,
    [Fecha_UltMod]      DATETIME      NULL,
    [IdUsuarioUltAnu]   VARCHAR (20)  NULL,
    [Fecha_UltAnu]      DATETIME      NULL,
    [nom_pc]            VARCHAR (50)  NULL,
    [Motivo_Anu]        VARCHAR (200) NULL,
    CONSTRAINT [PK_ro_marcaciones_x_empleado_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdRegistro] ASC),
    CONSTRAINT [FK_ro_marcaciones_x_empleado_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_marcaciones_x_empleado_ro_marcaciones_tipo] FOREIGN KEY ([IdTipoMarcaciones]) REFERENCES [dbo].[ro_marcaciones_tipo] ([IdTipoMarcaciones]),
    CONSTRAINT [FK_ro_marcaciones_x_empleado_ro_Nomina_Tipo] FOREIGN KEY ([IdEmpresa], [IdNomina]) REFERENCES [dbo].[ro_Nomina_Tipo] ([IdEmpresa], [IdNomina_Tipo]),
    CONSTRAINT [FK_ro_marcaciones_x_empleado_tb_sucursal] FOREIGN KEY ([IdEmpresa], [IdSucursal]) REFERENCES [dbo].[tb_sucursal] ([IdEmpresa], [IdSucursal])
);









