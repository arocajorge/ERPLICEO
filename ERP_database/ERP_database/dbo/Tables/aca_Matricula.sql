CREATE TABLE [dbo].[aca_Matricula] (
    [IdEmpresa]   INT           NOT NULL,
    [IdMatricula] NUMERIC (18)  NOT NULL,
    [Codigo]      VARCHAR (200) NULL,
    [IdAlumno]    NUMERIC (18)  NOT NULL,
    [IdAnio]      INT           NOT NULL,
    [IdSede]      INT           NOT NULL,
    [IdNivel]     INT           NOT NULL,
    [IdJornada]   INT           NOT NULL,
    [IdCurso]     INT           NOT NULL,
    [IdParalelo]  INT           NOT NULL,
    [IdPersonaF]  NUMERIC (18)  NOT NULL,
    [IdPersonaR]  NUMERIC (18)  NOT NULL,
    [IdPlantilla] INT           NOT NULL,
    CONSTRAINT [PK_aca_Matricula] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMatricula] ASC),
    CONSTRAINT [FK_aca_Matricula_aca_alumno] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_Alumno] ([IdEmpresa], [IdAlumno]),
    CONSTRAINT [FK_aca_Matricula_aca_AnioLectivo_Curso_Paralelo] FOREIGN KEY ([IdEmpresa], [IdAnio], [IdSede], [IdNivel], [IdJornada], [IdCurso], [IdParalelo]) REFERENCES [dbo].[aca_AnioLectivo_Curso_Paralelo] ([IdEmpresa], [IdAnio], [IdSede], [IdNivel], [IdJornada], [IdCurso], [IdParalelo]),
    CONSTRAINT [FK_aca_Matricula_aca_Plantilla] FOREIGN KEY ([IdEmpresa], [IdAnio], [IdPlantilla]) REFERENCES [dbo].[aca_Plantilla] ([IdEmpresa], [IdAnio], [IdPlantilla])
);

