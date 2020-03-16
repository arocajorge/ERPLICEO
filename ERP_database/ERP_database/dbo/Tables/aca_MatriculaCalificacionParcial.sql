CREATE TABLE [dbo].[aca_MatriculaCalificacionParcial] (
    [IdEmpresa]          INT             NOT NULL,
    [IdMatricula]        NUMERIC (18)    NOT NULL,
    [IdMateria]          INT             NOT NULL,
    [Parcial]            VARCHAR (10)    NOT NULL,
    [IdProfesor]         NUMERIC (18)    NULL,
    [Calificacion1]      NUMERIC (18, 2) NULL,
    [Calificacion2]      NUMERIC (18, 2) NULL,
    [Calificacion3]      NUMERIC (18, 2) NULL,
    [Calificacion4]      NUMERIC (18, 2) NULL,
    [Evaluacion]         NUMERIC (18, 2) NULL,
    [Remedial1]          NUMERIC (18, 2) NULL,
    [Remedial2]          NUMERIC (18, 2) NULL,
    [Conducta]           INT             NULL,
    [MotivoCalificacion] VARCHAR (MAX)   NULL,
    [MotivoConducta]     VARCHAR (MAX)   NULL,
    [AccionRemedial]     VARCHAR (MAX)   NULL,
    CONSTRAINT [PK_aca_MatriculaCalificacionParcial_1] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMatricula] ASC, [IdMateria] ASC, [Parcial] ASC),
    CONSTRAINT [FK_aca_MatriculaCalificacionParcial_aca_Materia] FOREIGN KEY ([IdEmpresa], [IdMateria]) REFERENCES [dbo].[aca_Materia] ([IdEmpresa], [IdMateria]),
    CONSTRAINT [FK_aca_MatriculaCalificacionParcial_aca_Matricula] FOREIGN KEY ([IdEmpresa], [IdMatricula]) REFERENCES [dbo].[aca_Matricula] ([IdEmpresa], [IdMatricula]),
    CONSTRAINT [FK_aca_MatriculaCalificacionParcial_aca_Profesor] FOREIGN KEY ([IdEmpresa], [IdProfesor]) REFERENCES [dbo].[aca_Profesor] ([IdEmpresa], [IdProfesor])
);

