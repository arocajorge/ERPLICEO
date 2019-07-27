CREATE TABLE [dbo].[ro_horario_planificacion] (
    [IdEmpresa]       INT           NOT NULL,
    [IdPlanificacion] NUMERIC (18)  NOT NULL,
    [IdNomina]        INT           NOT NULL,
    [FechaInicio]     DATE          NOT NULL,
    [FechaFin]        DATE          NOT NULL,
    [Observacion]     VARCHAR (500) NULL,
    [Estado]          CHAR (1)      NOT NULL,
    [IdUsuario]       VARCHAR (20)  NULL,
    [Fecha_Transac]   DATETIME      NULL,
    [IdUsuarioUltMod] VARCHAR (20)  NULL,
    [Fecha_UltMod]    DATETIME      NULL,
    [IdUsuarioUltAnu] VARCHAR (20)  NULL,
    [Fecha_UltAnu]    DATETIME      NULL,
    [nom_pc]          VARCHAR (50)  NULL,
    [ip]              VARCHAR (25)  NULL,
    [MotivoAnulacion] VARCHAR (100) NULL,
    CONSTRAINT [PK_ro_horario_planificacion_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPlanificacion] ASC),
    CONSTRAINT [FK_ro_horario_planificacion_tb_empresa] FOREIGN KEY ([IdEmpresa]) REFERENCES [dbo].[tb_empresa] ([IdEmpresa])
);

