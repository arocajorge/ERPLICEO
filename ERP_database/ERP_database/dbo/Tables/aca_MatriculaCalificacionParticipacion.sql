CREATE TABLE [dbo].[aca_MatriculaCalificacionParticipacion] (
    [IdEmpresa]             INT             NOT NULL,
    [IdMatricula]           NUMERIC (18)    NOT NULL,
    [IdAlumno]              NUMERIC (18)    NOT NULL,
    [IdTematica]            INT             NOT NULL,
    [IdCampoAccion]         INT             NOT NULL,
    [IdProfesor]            NUMERIC (18)    NULL,
    [CalificacionP1]        NUMERIC (18, 2) NULL,
    [CalificacionP2]        NUMERIC (18, 2) NULL,
    [PromedioQ1]            NUMERIC (18, 2) NULL,
    [CalificacionP3]        NUMERIC (18, 2) NULL,
    [CalificacionP4]        NUMERIC (18, 2) NULL,
    [PromedioQ2]            NUMERIC (18, 2) NULL,
    [PromedioFinal]         NUMERIC (18, 2) NULL,
    [IdUsuarioCreacion]     VARCHAR (200)   NULL,
    [FechaCreacion]         DATETIME        NULL,
    [IdUsuarioModificacion] VARCHAR (200)   NULL,
    [FechaModificacion]     DATETIME        NULL,
    CONSTRAINT [PK_aca_MatriculaCalificacionParticipacion] PRIMARY KEY CLUSTERED ([IdEmpresa] ASC, [IdMatricula] ASC),
    CONSTRAINT [FK_aca_MatriculaCalificacionParticipacion_aca_Alumno] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_Alumno] ([IdEmpresa], [IdAlumno]),
    CONSTRAINT [FK_aca_MatriculaCalificacionParticipacion_aca_Alumno1] FOREIGN KEY ([IdEmpresa], [IdAlumno]) REFERENCES [dbo].[aca_Alumno] ([IdEmpresa], [IdAlumno]),
    CONSTRAINT [FK_aca_MatriculaCalificacionParticipacion_aca_CampoAccion] FOREIGN KEY ([IdEmpresa], [IdCampoAccion]) REFERENCES [dbo].[aca_CampoAccion] ([IdEmpresa], [IdCampoAccion]),
    CONSTRAINT [FK_aca_MatriculaCalificacionParticipacion_aca_Matricula] FOREIGN KEY ([IdEmpresa], [IdMatricula]) REFERENCES [dbo].[aca_Matricula] ([IdEmpresa], [IdMatricula]),
    CONSTRAINT [FK_aca_MatriculaCalificacionParticipacion_aca_Matricula1] FOREIGN KEY ([IdEmpresa], [IdMatricula]) REFERENCES [dbo].[aca_Matricula] ([IdEmpresa], [IdMatricula]),
    CONSTRAINT [FK_aca_MatriculaCalificacionParticipacion_aca_Profesor] FOREIGN KEY ([IdEmpresa], [IdProfesor]) REFERENCES [dbo].[aca_Profesor] ([IdEmpresa], [IdProfesor]),
    CONSTRAINT [FK_aca_MatriculaCalificacionParticipacion_aca_Tematica] FOREIGN KEY ([IdEmpresa], [IdTematica]) REFERENCES [dbo].[aca_Tematica] ([IdEmpresa], [IdTematica])
);







