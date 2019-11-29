CREATE TABLE [dbo].[aca_AnioLectivo_Paralelo_Profesor] (
    [IdEmpresa]  INT          NOT NULL,
    [IdAnio]     INT          NOT NULL,
    [IdSede]     INT          NOT NULL,
    [IdNivel]    INT          NOT NULL,
    [IdJornada]  INT          NOT NULL,
    [IdCurso]    INT          NOT NULL,
    [IdMateria]  INT          NOT NULL,
    [IdParalelo] INT          NOT NULL,
    [IdProfesor] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_aca_AnioLectivo_Paralelo_Profesor] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdSede] ASC, [IdNivel] ASC, [IdJornada] ASC, [IdCurso] ASC, [IdMateria] ASC, [IdParalelo] ASC, [IdProfesor] ASC),
    CONSTRAINT [FK_aca_AnioLectivo_Paralelo_Profesor_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio]),
    CONSTRAINT [FK_aca_AnioLectivo_Paralelo_Profesor_aca_Curso] FOREIGN KEY ([IdEmpresa], [IdCurso]) REFERENCES [dbo].[aca_Curso] ([IdEmpresa], [IdCurso]),
    CONSTRAINT [FK_aca_AnioLectivo_Paralelo_Profesor_aca_Jornada] FOREIGN KEY ([IdEmpresa], [IdJornada]) REFERENCES [dbo].[aca_Jornada] ([IdEmpresa], [IdJornada]),
    CONSTRAINT [FK_aca_AnioLectivo_Paralelo_Profesor_aca_Materia] FOREIGN KEY ([IdEmpresa], [IdMateria]) REFERENCES [dbo].[aca_Materia] ([IdEmpresa], [IdMateria]),
    CONSTRAINT [FK_aca_AnioLectivo_Paralelo_Profesor_aca_NivelAcademico] FOREIGN KEY ([IdEmpresa], [IdNivel]) REFERENCES [dbo].[aca_NivelAcademico] ([IdEmpresa], [IdNivel]),
    CONSTRAINT [FK_aca_AnioLectivo_Paralelo_Profesor_aca_Paralelo] FOREIGN KEY ([IdEmpresa], [IdParalelo]) REFERENCES [dbo].[aca_Paralelo] ([IdEmpresa], [IdParalelo]),
    CONSTRAINT [FK_aca_AnioLectivo_Paralelo_Profesor_aca_Profesor] FOREIGN KEY ([IdEmpresa], [IdProfesor]) REFERENCES [dbo].[aca_Profesor] ([IdEmpresa], [IdProfesor]),
    CONSTRAINT [FK_aca_AnioLectivo_Paralelo_Profesor_aca_Sede] FOREIGN KEY ([IdEmpresa], [IdSede]) REFERENCES [dbo].[aca_Sede] ([IdEmpresa], [IdSede])
);







