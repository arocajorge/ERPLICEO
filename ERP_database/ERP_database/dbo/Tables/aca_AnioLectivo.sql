CREATE TABLE [dbo].[aca_AnioLectivo] (
    [IdEmpresa]             INT           NOT NULL,
    [IdAnio]                INT           NOT NULL,
    [Descripcion]           VARCHAR (50)  NOT NULL,
    [FechaDesde]            DATE          NOT NULL,
    [FechaHasta]            DATE          NOT NULL,
    [Estado]                BIT           NOT NULL,
    [EnCurso]               BIT           NOT NULL,
    [BloquearMatricula]     BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_AnioLectivo] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC)
);





