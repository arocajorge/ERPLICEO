CREATE TABLE [dbo].[aca_AnioLectivo_Curso_Paralelo_Dece] (
    [IdEmpresa]             INT           NOT NULL,
    [IdAnio]                INT           NOT NULL,
    [IdSede]                INT           NOT NULL,
    [IdNivel]               INT           NOT NULL,
    [IdJornada]             INT           NOT NULL,
    [IdCurso]               INT           NOT NULL,
    [IdParalelo]            INT           NOT NULL,
    [ObservacionQ1]         VARCHAR (MAX) NULL,
    [ObservacionQ2]         VARCHAR (MAX) NULL,
    [Estado]                BIT           NOT NULL,
    [IdUsuarioCreacion]     VARCHAR (200) NULL,
    [FechaCreacion]         DATETIME      NULL,
    [IdUsuarioModificacion] VARCHAR (200) NULL,
    [FechaModificacion]     DATETIME      NULL,
    [IdUsuarioAnulacion]    VARCHAR (200) NULL,
    [FechaAnulacion]        DATETIME      NULL,
    [MotivoAnulacion]       VARCHAR (MAX) NULL,
    CONSTRAINT [PK_aca_AnioLectivo_Curso_Paralelo_Dece] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdSede] ASC, [IdNivel] ASC, [IdJornada] ASC, [IdCurso] ASC, [IdParalelo] ASC)
);

