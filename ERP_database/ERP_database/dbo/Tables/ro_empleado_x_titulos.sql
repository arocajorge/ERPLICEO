CREATE TABLE [dbo].[ro_empleado_x_titulos] (
    [IdEmpresa]       INT           NOT NULL,
    [IdEmpleado]      NUMERIC (18)  NOT NULL,
    [Secuencia]       INT           NOT NULL,
    [fecha]           DATETIME      NOT NULL,
    [IdInstitucion]   VARCHAR (10)  NOT NULL,
    [IdTitulo]        VARCHAR (10)  NOT NULL,
    [Observacion]     VARCHAR (50)  NOT NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [nom_pc]          VARCHAR (50)  NULL,
    [ip]              VARCHAR (25)  NULL,
    [MotivoAnulacion] VARCHAR (100) NULL,
    [estado]          VARCHAR (1)   NULL,
    CONSTRAINT [PK_ro_empleado_x_titulos] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdEmpleado] ASC, [Secuencia] ASC),
    CONSTRAINT [FK_ro_empleado_x_titulos_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado])
);

