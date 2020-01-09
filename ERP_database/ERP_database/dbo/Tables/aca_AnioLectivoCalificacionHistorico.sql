CREATE TABLE [dbo].[aca_AnioLectivoCalificacionHistorico] (
    [IdEmpresa] INT             NOT NULL,
    [IdAnio]    INT             NOT NULL,
    [IdAlumno]  NUMERIC (18)    NOT NULL,
    [IdCurso]   INT             NOT NULL,
    [Promedio]  NUMERIC (18, 2) NOT NULL,
    [Conducta]  NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_aca_AnioLectivoCalificacionHistorico] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdAnio] ASC, [IdAlumno] ASC),
    CONSTRAINT [FK_aca_AnioLectivoCalificacionHistorico_aca_Alumno] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_Alumno] ([IdEmpresa], [IdAlumno]),
    CONSTRAINT [FK_aca_AnioLectivoCalificacionHistorico_aca_AnioLectivo] FOREIGN KEY ([IdEmpresa], [IdAnio]) REFERENCES [dbo].[aca_AnioLectivo] ([IdEmpresa], [IdAnio])
);

