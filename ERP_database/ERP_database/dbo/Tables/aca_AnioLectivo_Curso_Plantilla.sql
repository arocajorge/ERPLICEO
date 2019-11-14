CREATE TABLE [dbo].[aca_AnioLectivo_Curso_Plantilla] (
    [IdEmpresa]   INT         NOT NULL,
    [IdAnio]      INT         NOT NULL,
    [IdSede]      INT         NOT NULL,
    [IdNivel]     INT         NOT NULL,
    [IdJornada]   INT         NOT NULL,
    [IdCurso]     INT         NOT NULL,
    [IdPlantilla] INT         NOT NULL,
    [Observacion] VARCHAR (1) NULL,
    CONSTRAINT [PK_aca_AnioLectivo_Curso_Plantilla] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdSede] ASC, [IdNivel] ASC, [IdJornada] ASC, [IdCurso] ASC, [IdPlantilla] ASC),
    CONSTRAINT [FK_aca_AnioLectivo_Curso_Plantilla_aca_Plantilla] FOREIGN KEY ([IdEmpresa], [IdAnio], [IdPlantilla]) REFERENCES [dbo].[aca_Plantilla] ([IdEmpresa], [IdAnio], [IdPlantilla])
);

