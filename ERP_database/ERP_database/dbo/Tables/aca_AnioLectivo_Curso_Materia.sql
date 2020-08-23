CREATE TABLE [dbo].[aca_AnioLectivo_Curso_Materia] (
    [IdEmpresa]                  INT           NOT NULL,
    [IdAnio]                     INT           NOT NULL,
    [IdSede]                     INT           NOT NULL,
    [IdNivel]                    INT           NOT NULL,
    [IdJornada]                  INT           NOT NULL,
    [IdCurso]                    INT           NOT NULL,
    [IdMateria]                  INT           NOT NULL,
    [NomMateria]                 VARCHAR (500) NOT NULL,
    [NomMateriaArea]             VARCHAR (500) NULL,
    [NomMateriaGrupo]            VARCHAR (500) NULL,
    [IdCatalogoTipoCalificacion] INT           NULL,
    [EsObligatorio]              BIT           NOT NULL,
    [OrdenMateria]               INT           NOT NULL,
    [OrdenMateriaGrupo]          INT           NULL,
    [OrdenMateriaArea]           INT           NULL,
    CONSTRAINT [PK_aca_AnioLectivo_Curso_Materia] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdSede] ASC, [IdNivel] ASC, [IdJornada] ASC, [IdCurso] ASC, [IdMateria] ASC),
    CONSTRAINT [FK_aca_AnioLectivo_Curso_Materia_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio]),
    CONSTRAINT [FK_aca_AnioLectivo_Curso_Materia_aca_Curso] FOREIGN KEY ([IdEmpresa], [IdCurso]) REFERENCES [dbo].[aca_Curso] ([IdEmpresa], [IdCurso]),
    CONSTRAINT [FK_aca_AnioLectivo_Curso_Materia_aca_Jornada] FOREIGN KEY ([IdEmpresa], [IdJornada]) REFERENCES [dbo].[aca_Jornada] ([IdEmpresa], [IdJornada]),
    CONSTRAINT [FK_aca_AnioLectivo_Curso_Materia_aca_Materia] FOREIGN KEY ([IdEmpresa], [IdMateria]) REFERENCES [dbo].[aca_Materia] ([IdEmpresa], [IdMateria]),
    CONSTRAINT [FK_aca_AnioLectivo_Curso_Materia_aca_NivelAcademico] FOREIGN KEY ([IdEmpresa], [IdNivel]) REFERENCES [dbo].[aca_NivelAcademico] ([IdEmpresa], [IdNivel]),
    CONSTRAINT [FK_aca_AnioLectivo_Curso_Materia_aca_Sede] FOREIGN KEY ([IdEmpresa], [IdSede]) REFERENCES [dbo].[aca_Sede] ([IdEmpresa], [IdSede])
);











