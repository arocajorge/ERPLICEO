CREATE TABLE [dbo].[ro_contrato] (
    [IdEmpresa]       INT           NOT NULL,
    [IdEmpleado]      NUMERIC (18)  NOT NULL,
    [IdContrato]      NUMERIC (18)  NOT NULL,
    [IdNomina]        INT           NULL,
    [IdContrato_Tipo] VARCHAR (10)  NOT NULL,
    [NumDocumento]    VARCHAR (25)  NOT NULL,
    [FechaInicio]     DATE          NOT NULL,
    [FechaFin]        DATE          NOT NULL,
    [Sueldo]          FLOAT (53)    NULL,
    [Estado]          CHAR (1)      NOT NULL,
    [IdUsuario]       VARCHAR (25)  NULL,
    [Observacion]     VARCHAR (100) NULL,
    [MotiAnula]       VARCHAR (200) NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (25)  NULL,
    [FechaHoraAnul]   DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (25)  NULL,
    [EstadoContrato]  VARCHAR (10)  NULL,
    CONSTRAINT [PK_ro_contrato] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdEmpleado] ASC, [IdContrato] ASC),
    CONSTRAINT [FK_ro_contrato_ro_catalogo] FOREIGN KEY ([EstadoContrato]) REFERENCES [dbo].[ro_catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_ro_contrato_ro_catalogo1] FOREIGN KEY ([IdContrato_Tipo]) REFERENCES [dbo].[ro_catalogo] ([CodCatalogo]),
    CONSTRAINT [FK_ro_contrato_ro_empleado] FOREIGN KEY ([IdEmpresa], [IdEmpleado]) REFERENCES [dbo].[ro_empleado] ([IdEmpresa], [IdEmpleado]),
    CONSTRAINT [FK_ro_contrato_ro_Nomina_Tipo] FOREIGN KEY ([IdEmpresa], [IdNomina]) REFERENCES [dbo].[ro_Nomina_Tipo] ([IdEmpresa], [IdNomina_Tipo])
);







