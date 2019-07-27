CREATE TABLE [dbo].[pre_PresupuestoPeriodo] (
    [IdEmpresa]             INT           NOT NULL,
    [IdPeriodo]             NUMERIC (18)  NOT NULL,
    [Observacion]           VARCHAR (MAX) NULL,
    [FechaInicio]           DATE          NOT NULL,
    [FechaFin]              DATE          NOT NULL,
    [DescripciónPeriodo]    VARCHAR (MAX) NOT NULL,
    [EstadoCierre]          BIT           NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (50)  NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (50)  NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (50)  NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_pre_Presupuesto] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPeriodo] ASC)
);



