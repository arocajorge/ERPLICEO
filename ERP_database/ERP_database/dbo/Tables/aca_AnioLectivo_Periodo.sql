CREATE TABLE [dbo].[aca_AnioLectivo_Periodo] (
    [IdEmpresa]             INT           NOT NULL,
    [IdPeriodo]             INT           NOT NULL,
    [IdAnio]                INT           NOT NULL,
    [IdMes]                 INT           NOT NULL,
    [FechaDesde]            DATE          NOT NULL,
    [FechaHasta]            DATE          NOT NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_AnioLectivo_Periodo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdPeriodo] ASC),
    CONSTRAINT [FK_aca_AnioLectivo_Periodo_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio])
);

