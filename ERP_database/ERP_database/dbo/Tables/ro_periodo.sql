CREATE TABLE [dbo].[ro_periodo] (
    [IdEmpresa]             INT           NOT NULL,
    [IdPeriodo]             INT           NOT NULL,
    [pe_anio]               INT           NULL,
    [pe_mes]                INT           NULL,
    [pe_FechaIni]           SMALLDATETIME NOT NULL,
    [pe_FechaFin]           SMALLDATETIME NOT NULL,
    [pe_estado]             NVARCHAR (1)  NOT NULL,
    [Fecha_Transac]         DATETIME      NULL,
    [Fecha_UltMod]          DATETIME      NULL,
    [IdUsuarioUltMod]       VARCHAR (25)  NULL,
    [FechaHoraAnul]         DATETIME      NULL,
    [IdUsuarioUltAnu]       VARCHAR (25)  NULL,
    [MotivoAnulacion]       VARCHAR (100) NULL,
    [Cod_region]            VARCHAR (10)  NULL,
    [Carga_Todos_Empleados] BIT           NULL,
    [IdUsuario]             VARCHAR (25)  NULL,
    CONSTRAINT [PK_ro_periodo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPeriodo] ASC)
);

