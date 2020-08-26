CREATE TABLE [dbo].[aca_AnioLectivoCalificacionHistorico] (
    [IdEmpresa]              INT             NOT NULL,
    [IdAnio]                 INT             NOT NULL,
    [IdAlumno]               NUMERIC (18)    NOT NULL,
    [IdNivel]                INT             NOT NULL,
    [IdCurso]                INT             NOT NULL,
    [AntiguaInstitucion]     VARCHAR (500)   NOT NULL,
    [Promedio]               NUMERIC (18, 2) NOT NULL,
    [IdEquivalenciaPromedio] INT             NULL,
    [Conducta]               NUMERIC (18, 2) NOT NULL,
    [SecuenciaConducta]      INT             NULL,
    CONSTRAINT [PK_aca_AnioLectivoCalificacionHistorico] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdAlumno] ASC),
    CONSTRAINT [FK_aca_AnioLectivoCalificacionHistorico_aca_Alumno] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_Alumno] ([IdEmpresa], [IdAlumno]),
    CONSTRAINT [FK_aca_AnioLectivoCalificacionHistorico_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio]),
    CONSTRAINT [FK_aca_AnioLectivoCalificacionHistorico_aca_AnioLectivoConductaEquivalencia] FOREIGN KEY ([IdEmpresa], [IdAnio], [SecuenciaConducta]) REFERENCES [dbo].[aca_AnioLectivoConductaEquivalencia] ([IdEmpresa], [IdAnio], [Secuencia]),
    CONSTRAINT [FK_aca_AnioLectivoCalificacionHistorico_aca_AnioLectivoEquivalenciaPromedio] FOREIGN KEY ([IdEmpresa], [IdEquivalenciaPromedio], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivoEquivalenciaPromedio] ([IdEmpresa], [IdEquivalenciaPromedio], [IdAnio]),
    CONSTRAINT [FK_aca_AnioLectivoCalificacionHistorico_aca_Curso] FOREIGN KEY ([IdEmpresa], [IdCurso]) REFERENCES [dbo].[aca_Curso] ([IdEmpresa], [IdCurso]),
    CONSTRAINT [FK_aca_AnioLectivoCalificacionHistorico_aca_NivelAcademico] FOREIGN KEY ([IdEmpresa], [IdNivel]) REFERENCES [dbo].[aca_NivelAcademico] ([IdEmpresa], [IdNivel])
);





